using System;
using System.Runtime.InteropServices;

namespace StanleySaveEditor.Platform {

    public enum SupportedPlatform {
        Windows,
        Linux,
        MacOS,
        Unknown,
    }

    public abstract class PlatformAPI {
        public abstract string GetGameDataDirectory();

        #region Internal API

        private static PlatformAPI s_currentAPI = null;

        public static SupportedPlatform GetPlatform() {

            if ( RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ) {
                return SupportedPlatform.Windows;
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                return SupportedPlatform.MacOS;
            } else if ( RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ) {
                return SupportedPlatform.Linux;
            }

            return SupportedPlatform.Unknown;
        }

        /// <summary>
        /// Returns an implementation of the current platform's native APIs
        /// </summary>
        public static PlatformAPI GetImplementation() {
            if ( s_currentAPI == null ) {
                var currentPlatform = GetPlatform();
                switch ( currentPlatform ) {
                    case SupportedPlatform.Windows:
                        s_currentAPI = new Platform_Win64();
                        break;
                    default:
                        throw new ApplicationException($"Unsupported platform {currentPlatform}!");
                }
            }

            return s_currentAPI;
        }
        
        #endregion
    }
}
