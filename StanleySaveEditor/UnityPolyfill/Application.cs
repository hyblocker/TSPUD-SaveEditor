using StanleySaveEditor.Platform;

namespace UnityEngine {
    public class Application
    {
        public static string persistentDataPath {
            get {
                return PlatformAPI.GetImplementation().GetGameDataDirectory();
            }
        }
    }
}
