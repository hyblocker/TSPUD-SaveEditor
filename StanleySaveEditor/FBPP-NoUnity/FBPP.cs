using System.Text;
using System.IO;
using System;
using UnityEngine;

public static class FBPP
{
    private class FBPPInitException : Exception
    {
        public FBPPInitException(string message) : base(message)
        { }
    }

    private const string INIT_EXCEPTION_MESSAGE = "Error, you must call FBPP.Start(FBPPConfig config) before trying to get or set saved data.";

    private static FBPPConfig s_config;

    public static bool ShowInitWarning = true;

    private static FBPPFileModel s_latestData;
    private static StringBuilder s_sb = new StringBuilder();

    const string String_Empty = "";

    #region Init

    public static void Start(FBPPConfig config)
    {
        s_config = config;
        s_latestData = GetSaveFile();
    }

    private static void CheckForInit()
    {
        if (s_config == null)
        {
            throw new FBPPInitException(INIT_EXCEPTION_MESSAGE);
        }
    }

    #endregion

    #region Public Get, Set and util

    public static void SetString(string key, string value = String_Empty)
    {
        AddDataToSaveFile(key, value);
    }

    public static string GetString(string key, string defaultValue = String_Empty)
    {
        return (string)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetInt(string key, int value = default(int))
    {
        AddDataToSaveFile(key, value);
    }

    public static int GetInt(string key, int defaultValue = default(int))
    {
        return (int)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetFloat(string key, float value = default(float))
    {
        AddDataToSaveFile(key, value);
    }

    public static float GetFloat(string key, float defaultValue = default(float))
    {
        return (float)GetDataFromSaveFile(key, defaultValue);
    }

    public static void SetBool(string key, bool value = default(bool))
    {
        AddDataToSaveFile(key, value);
    }

    public static bool GetBool(string key, bool defaultValue = default(bool))
    {
        return (bool)GetDataFromSaveFile(key, defaultValue);
    }

    public static bool HasKey(string key)
    {
        return GetSaveFile().HasKey(key);
    }

    public static bool HasKeyForString(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, string.Empty);
    }

    public static bool HasKeyForInt(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(int));
    }

    public static bool HasKeyForFloat(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(float));
    }

    public static bool HasKeyForBool(string key)
    {
        return GetSaveFile().HasKeyFromObject(key, default(bool));
    }

    public static void DeleteKey(string key)
    {
        GetSaveFile().DeleteKey(key);
        SaveSaveFile();
    }

    public static void DeleteString(string key)
    {
        GetSaveFile().DeleteString(key);
        SaveSaveFile();
    }

    public static void DeleteInt(string key)
    {
        GetSaveFile().DeleteInt(key);
        SaveSaveFile();
    }

    public static void DeleteFloat(string key)
    {
        GetSaveFile().DeleteFloat(key);
        SaveSaveFile();
    }

    public static void DeleteBool(string key)
    {
        GetSaveFile().DeleteBool(key);
        SaveSaveFile();
    }

    public static void DeleteAll()
    {
        WriteToSaveFile(JsonUtility.ToJson(new FBPPFileModel()));
        s_latestData = new FBPPFileModel();
    }

    public static void OverwriteLocalSaveFile(string data)
    {
        WriteToSaveFile(data);
        s_latestData = null;
        s_latestData = GetSaveFile();
    }


    #endregion

    #region Read data

    internal static FBPPFileModel GetSaveFile()
    {
        CheckForInit();
        CheckSaveFileExists();
        if (s_latestData == null)
        {
            var saveFileText = File.ReadAllText(GetSaveFilePath());
            if (s_config.ScrambleSaveData)
            {
                saveFileText = DataScrambler(saveFileText);
            }
            try
            {
                s_latestData = JsonUtility.FromJson<FBPPFileModel>(saveFileText);
            }
            catch (ArgumentException e)
            {
                Console.Error.WriteLine(new Exception("FBPP Error loading save file: " + e.Message).StackTrace);
                if (s_config.OnLoadError != null)
                {
                    s_config.OnLoadError.Invoke();
                }
                else
                {
                    DeleteAll();
                }
            }
        }
        return s_latestData;
    }

    public static string GetSaveFilePath()
    {
        CheckForInit();
        return Path.Combine(s_config.GetSaveFilePath(), s_config.SaveFileName);
    }



    public static string GetSaveFileAsJson()
    {
        CheckForInit();
        CheckSaveFileExists();
        return JsonUtility.ToJson(GetSaveFile());
    }

    private static object GetDataFromSaveFile(string key, object defaultValue)
    {
        return GetSaveFile().GetValueForKey(key, defaultValue);
    }

    #endregion

    #region Write data

    private static void AddDataToSaveFile(string key, object value)
    {
        CheckForInit();
        GetSaveFile().UpdateOrAddData(key, value);
        SaveSaveFile();
    }

    public static void Save()
    {
        CheckForInit();
        SaveSaveFile(true);
    }

    private static void SaveSaveFile(bool manualSave = false)
    {
        if (s_config.AutoSaveData || manualSave)
        {
            WriteToSaveFile(JsonUtility.ToJson(GetSaveFile()));
        }
    }
    private static void WriteToSaveFile(string data)
    {
        var tw = new StreamWriter(GetSaveFilePath());
        if (s_config.ScrambleSaveData)
        {
            data = DataScrambler(data);
        }
        tw.Write(data);
        tw.Close();
    }

    #endregion

    #region File Utils

    private static void CheckSaveFileExists()
    {
        if (!DoesSaveFileExist())
        {
            CreateNewSaveFile();
        }
    }

    private static bool DoesSaveFileExist()
    {
        return File.Exists(GetSaveFilePath());
    }

    private static void CreateNewSaveFile()
    {
        WriteToSaveFile(JsonUtility.ToJson(new FBPPFileModel()));
    }

    internal static string DataScrambler(string data)
    {
        s_sb.Clear();

        for (int i = 0; i < data.Length; i++)
        {
            s_sb.Append((char)(data[i] ^ s_config.EncryptionSecret[i % s_config.EncryptionSecret.Length]));
        }
        return s_sb.ToString();
    }

    #endregion
}


