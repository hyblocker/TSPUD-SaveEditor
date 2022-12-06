using Newtonsoft.Json;
using System;
using System.IO;

namespace StanleySaveEditor {
    public static class SaveHandler
    {
        private static StanleyParableSaveContainer currentSave;

        public static void Init()
        {
            currentSave = StanleyParableSaveManager.LoadSaveToMemory();
        }

        /// <summary>
        /// Writes a save file to the disk
        /// </summary>
        /// <param name="path">Where to write the save file to</param>
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

        /// <summary>
        /// Loads a file from disk
        /// </summary>
        /// <param name="path">Where to load the file from</param>
        public static void Load(string path)
        {
            if (currentSave == null)
                throw new Exception("Save is null! Have you called Init()?");
            path = Path.GetFullPath(path);
            var json = File.ReadAllText(path);
            var newSave = JsonConvert.DeserializeObject<StanleyParableSaveContainer>(json);
            currentSave = newSave;
            currentSave.Save();
        }
    }
}
