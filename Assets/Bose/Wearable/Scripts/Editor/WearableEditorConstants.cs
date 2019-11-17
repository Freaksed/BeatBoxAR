
using UnityEngine;

namespace Bose.Wearable.Editor
{
	/// <summary>
	/// An internal helper class to contain constants for editor usage.
	/// </summary>
	internal static class WearableEditorConstants
	{
		// Logs
		public const string LOCAL_PDF_NOT_FOUND
			= "[Bose Wearable] Could not find the local PDF documentation with GUID [{0}] to open, please reimport this asset.";

		public const string LOCAL_LICENSE_NOT_FOUND
			= "[Bose Wearable] Could not find the local license with GUID [{0}] to open, please reimport this asset.";

		// Editor Prefs
		public const string SHOW_BUILD_ON_SUCCESS_KEY = "bose_show_build_on_success_key";
		public const bool DEFAULT_SHOW_BUILD_ON_SUCCESS_PREF = true;

		// Inspector
		public const float SINGLE_LINE_HEIGHT = 20f;
		public static readonly GUILayoutOption[] EMPTY_LAYOUT_OPTIONS;

		public const string DEVICE_SPECIFIC_GESTURE_DISCOURAGED_WARNING =
			"Use of device-specific gestures is discouraged as it may not be supported on all Bose AR devices. " +
			"Consider using a device-agnostic gesture.";

		#region Platform Constants: iOS

		// Post Build Processor
		public const int XCODE_PRE_BUILD_PROCESSOR_ORDER = 0;
		public const int XCODE_POST_BUILD_PROCESSOR_ORDER = 0;

		// Xcode Workspace
		public const string XCODE_PROJECT_NAME = "Unity-iPhone.xcodeproj/project.pbxproj";

		// Framework path was broken in 2018.3.0->2018.3.3
		// From 2018.3.4 Release notes:
		// *  iOS: Fixed iOS Frameworks location is ignored when building Xcode project. (1108970)
		#if UNITY_2018_3_0 || UNITY_2018_3_1 || UNITY_2018_3_2 || UNITY_2018_3_3

		public const string XCODE_PROJECT_FRAMEWORKS_PATH = "Frameworks";

		#else

		public const string XCODE_PROJECT_FRAMEWORKS_PATH = "Frameworks/Bose/Wearable/Plugins/iOS/BoseWearable";

		#endif

		// Messages
		public const string ARCHITECTURE_ALTERATION_WARNING_WITH_MESSAGE = "[Bose Wearable] iOS Architecture forced to 'ARM64'. " +
		                                                               "Was set to '{0}'.";
		public const string OS_VERSION_ALTERATION_WARNING_WITH_MESSAGE = "[Bose Wearable] iOS Minimum Version forced to '{0}'. Was set to '{1}'.";
		public const string OS_VERSION_COMPILE_WARNING_WITH_MESSAGE
			= "[Bose Wearable] The iOS minimum compilable version is {0}, but will fail at runtime when the " +
			  "SDK is initialized. To ensure Bose AR runtime functionality please set your iOS minimum version " +
			  "to {1} or greater.";

		public const string OS_BLUETOOTH_ALTERATION_WARNING = "[Bose Wearable] Background Mode forced to allow connections to BLE devices.";

		// Info.plist properties
		public const string XCODE_INFO_PLIST_RELATIVE_PATH = "./Info.plist";
		public const string XCODE_INFO_PLIST_BLUETOOTH_PERIPHERAL_KEY = "NSBluetoothPeripheralUsageDescription";
		public const string XCODE_INFO_PLIST_BLUETOOTH_ALWAYS_KEY = "NSBluetoothAlwaysUsageDescription";
		public const string XCODE_INFO_PLIST_BLUETOOTH_MESSAGE = "This app uses Bluetooth to communicate with Bose AR devices.";
		public const string XCODE_INFO_PLIST_ALTERATION_WARNING_WITH_MESSAGE = "[Bose Wearable] Added missing property to Info.plist: {0}: {1}";

		// Xcode Build Properties and Values
		public const string XCODE_BUILD_PROPERTY_ENABLE_VALUE = "YES";
		public const string XCODE_BUILD_PROPERTY_DISABLE_VALUE = "NO";
		public const string XCODE_BUILD_PROPERTY_BIT_CODE_KEY = "ENABLE_BITCODE";
		public const string XCODE_BUILD_PROPERTY_MODULES_KEY = "CLANG_ENABLE_MODULES";
		public const string XCODE_BUILD_PROPERTY_SEARCH_PATHS_KEY = "LD_RUNPATH_SEARCH_PATHS";
		public const string XCODE_BUILD_PROPERTY_SEARCH_PATHS_VALUE = "@executable_path/Frameworks";
		public const string XCODE_BUILD_PROPERTY_EMBED_SWIFT_KEY = "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES";
		public const string XCODE_BUILD_PROPERTY_SWIFT_VERSION_KEY = "SWIFT_VERSION";
		public const string XCODE_BUILD_PROPERTY_SWIFT_VERSION_VALUE = "5.0";
		public const string XCODE_BUILD_PROPERTY_SWIFT_OPTIMIZATION_KEY = "SWIFT_OPTIMIZATION_LEVEL";
		public const string XCODE_BUILD_PROPERTY_SWIFT_OPTIMIZATION_VALUE = "-Onone";

		// Framework Path and Names
		public const string FRAMEWORK_FILE_FILTER = "*.framework";

		/// <summary>
		/// This corresponds to the location of the native plugins in the Unity project.
		/// </summary>
		public const string NATIVE_ARTIFACTS_PATH = "Bose/Wearable/Plugins/iOS/BoseWearable/";

		#endregion

		#region Platform Constants: Android

		// Build Order
		public const int ANDROID_PRE_BUILD_PROCESSOR_ORDER = 0;

		// Build Tools Messages
		public const string BUILD_TOOLS_UNSUPPORTED_PLATFORM_WARNING = "[Bose Wearable] Trying to build for unsupported platform {0}.";

		// Messages
		public const string ANDROID_VERSION_ALTERATION_WARNING_WITH_MESSAGE = "[Bose Wearable] Android minimum SDK version forced to '{0}'. Was set to '{1}'.";
		public const string ANDROID_VERSION_COMPILE_WARNING_WITH_MESSAGE
			= "[Bose Wearable] The Android minimum compilable SDK version is {0}, but will fail at runtime when the " +
			  "SDK is initialized. To ensure Bose AR runtime functionality please set your Android minimum SDK version " +
			  "to {1} or greater.";

		#endregion

		static WearableEditorConstants()
		{
			EMPTY_LAYOUT_OPTIONS = new GUILayoutOption[0];
		}
	}
}
