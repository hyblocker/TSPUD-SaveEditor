using System.ComponentModel;

namespace StanleySaveEditor
{
    public class StanleyParableSave
    {
        public StanleyParableSaveCacheEntry[] saveDataCache { get; set; }
    }

    public class StanleyParableSaveCacheEntry
    {
        [Category("Key")]
        public string key { get; set; }
        [Category("Key")]
        public ConfigurableTypeEnum configureableType { get; set; }
        [Category("Data")]
        public int IntValue { get; set; }
        [Category("Data")]
        public float FloatValue { get; set; }
        [Category("Data")]
        public bool BooleanValue { get; set; }
        [Category("Data")]
        public string StringValue { get; set; }

        public override string ToString()
        {
            return $"{{{key}}}";
        }
    }

    public enum ConfigurableTypeEnum
    {
        Integer = 0,
        Float = 1,
        Bool = 2,
        String = 3,
    }
}
