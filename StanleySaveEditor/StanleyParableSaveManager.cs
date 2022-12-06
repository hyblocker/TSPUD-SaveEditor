using Newtonsoft.Json;
using StanleySaveEditor.Platform;
using System.IO;
using System.Text;

namespace StanleySaveEditor {
    public class StanleyParableSaveContainer
    {
        public FBPPFileModel saveDataMisc;
        public StanleyParableSave saveData;

        public void Save()
        {
            string JsonSave = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            saveDataMisc.UpdateOrAddData("data", JsonSave);
            string coreSaveAsJson   = JsonConvert.SerializeObject(saveDataMisc, Formatting.None);
            string saveFileText     = StanleyParableSaveManager.Unscramble(coreSaveAsJson);
            string SaveFileJson     = Path.Combine(PlatformAPI.GetImplementation().GetGameDataDirectory(), "tspud-savedata.txt");
            File.WriteAllText(SaveFileJson, saveFileText);
        }
    }

    public class StanleyParableSaveManager
    {
        public static readonly string Key           = "saRpmZ6mMgZpmcojUkvkyGEQGez9YkWoXZfJdRc9ZmmJrCzfM8JksVxQfQK8uBBs";
        public static readonly string SaveFile      = Path.Combine(PlatformAPI.GetImplementation().GetGameDataDirectory(), "tspud-savedata.txt");
        public static readonly string SaveFileJson  = Path.Combine(PlatformAPI.GetImplementation().GetGameDataDirectory(), "tspud-savedata.json");

        static StringBuilder s_sb = new StringBuilder();

        public static StanleyParableSaveContainer LoadSaveToMemory()
        {
            string saveFileText         = File.ReadAllText(SaveFile);
            saveFileText                = Unscramble(saveFileText);

            File.WriteAllText(SaveFileJson, saveFileText);

            FBPPFileModel saveRoot      = JsonConvert.DeserializeObject<FBPPFileModel>(saveFileText);
            StanleyParableSave saveData = JsonConvert.DeserializeObject<StanleyParableSave>((string)saveRoot.GetValueForKey("data", "{\n    \"saveDataCache\": []\n}"));

            StanleyParableSaveContainer containerRoot = new StanleyParableSaveContainer();
            containerRoot.saveDataMisc  = saveRoot;
            containerRoot.saveData      = saveData;

            return containerRoot;
        }

        internal static string Unscramble(string data)
        {
            s_sb.Clear();

            for (int i = 0; i < data.Length; i++)
            {
                s_sb.Append((char)(data[i] ^ Key[i % Key.Length]));
            }
            return s_sb.ToString();
        }
    }
}
