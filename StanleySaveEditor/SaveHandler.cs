using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanleySaveEditor
{
    public static class SaveHandler
    {
        private static StanleyParableSaveContainer currentSave;

        public static void Init()
        {
            currentSave = StanleyParableSaveManager.LoadSaveToMemory();
        }

        public static void Save(string path)
        {
            if (currentSave == null)
                throw new Exception("Save is null! Have you called Init()?");
            var serializedSave = JsonConvert.SerializeObject(currentSave, Formatting.Indented);
            path = Path.GetFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, serializedSave);
        }

        public static void Load(string path)
        {
            if (currentSave == null)
                throw new Exception("Save is null! Have you called Init()?");
            var serializedSave = JsonConvert.SerializeObject(currentSave, Formatting.Indented);
            path = Path.GetFullPath(path);
            var json = File.ReadAllText(path);
            var newSave = JsonConvert.DeserializeObject<StanleyParableSaveContainer>(json);
            currentSave = newSave;
            currentSave.Save();
        }
    }
}
