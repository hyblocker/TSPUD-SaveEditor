using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace StanleySaveEditor
{
    public class StanleyParableSaveContainer
    {
        public FBPPFileModel saveDataMisc;
        public StanleyParableSave saveData;

        public void Save()
        {
            string JsonSave = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            saveDataMisc.UpdateOrAddData("data", JsonSave);
            string coreSaveAsJson = JsonConvert.SerializeObject(saveDataMisc, Formatting.None);
            var saveFileText = StanleyParableSaveManager.Unscramble(coreSaveAsJson);
            string SaveFileJson = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "Crows Crows Crows", "The Stanley Parable_ Ultra Deluxe", "tspud-savedata.txt");
            File.WriteAllText(SaveFileJson, saveFileText);
        }
    }

    public class StanleyParableSaveManager
    {
        public static readonly string Key = "saRpmZ6mMgZpmcojUkvkyGEQGez9YkWoXZfJdRc9ZmmJrCzfM8JksVxQfQK8uBBs";
        public static readonly string SaveFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "Crows Crows Crows", "The Stanley Parable_ Ultra Deluxe", "tspud-savedata.txt");
        public static readonly string SaveFileJson = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow", "Crows Crows Crows", "The Stanley Parable_ Ultra Deluxe", "tspud-savedata.json");

        public static StanleyParableSaveContainer LoadSaveToMemory()
        {
            var saveFileText = File.ReadAllText(SaveFile);
            saveFileText = Unscramble(saveFileText);
            File.WriteAllText(SaveFileJson, saveFileText);

            var saveRoot = JsonConvert.DeserializeObject<FBPPFileModel>(saveFileText);
            var saveData = JsonConvert.DeserializeObject<StanleyParableSave>((string)saveRoot.GetValueForKey("data", "{\n    \"saveDataCache\": []\n}"));

            StanleyParableSaveContainer containerRoot = new StanleyParableSaveContainer();
            containerRoot.saveDataMisc = saveRoot;
            containerRoot.saveData = saveData;

            return containerRoot;
        }
        static StringBuilder _sb = new StringBuilder();

        internal static string Unscramble(string data)
        {
            _sb.Clear();

            for (int i = 0; i < data.Length; i++)
            {
                _sb.Append((char)(data[i] ^ Key[i % Key.Length]));
            }
            return _sb.ToString();
        }
    }
}
