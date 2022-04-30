using System.ComponentModel;

namespace StanleySaveEditor
{
    public class StanleyParableSave
    {
        public StanleyParableSaveCacheEntry[] saveDataCache { get; set; }
    }

    public class StanleyParableSaveCacheEntry
    {
        [CategoryAttribute("Key")]
        public string key { get; set; }
        [CategoryAttribute("Key")]
        public int configureableType { get; set; }
        [CategoryAttribute("Data")]
        public int IntValue { get; set; }
        [CategoryAttribute("Data")]
        public float FloatValue { get; set; }
        [CategoryAttribute("Data")]
        public bool BooleanValue { get; set; }
        [CategoryAttribute("Data")]
        public string StringValue { get; set; }

        public string ToString()
        {
            return $"{{{key}}}";
        }
    }
}
