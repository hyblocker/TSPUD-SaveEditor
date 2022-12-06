using System;
using System.IO;

namespace StanleySaveEditor.Platform {
    public class Platform_Win64 : PlatformAPI {
        public override string GetGameDataDirectory() {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "AppData", "LocalLow", "Crows Crows Crows", "The Stanley Parable_ Ultra Deluxe"
            );
        }
    }
}
