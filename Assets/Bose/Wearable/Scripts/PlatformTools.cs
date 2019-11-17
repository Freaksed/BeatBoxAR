using UnityEngine;

namespace Bose.Wearable
{
	/// <summary>
	/// Helper methods for dealing with platform-specific logic not relevant for providers.
	/// </summary>
	public static class PlatformTools
	{
		/// <summary>
		/// Launches the Bose Connect application.
		/// </summary>
		public static void LaunchBoseConnectApp()
		{
			#if !UNITY_EDITOR && UNITY_IOS

			const string IOS_BOSE_CONNECT_URL = "https://apps.apple.com/us/app/bose-connect/id1046510029";

			Application.OpenURL(IOS_BOSE_CONNECT_URL);

			#elif !UNITY_EDITOR && UNITY_ANDROID

			const string ANDROID_BOSE_CONNECT_URL = "https://play.google.com/store/apps/details?id=com.bose.monet";

			Application.OpenURL(ANDROID_BOSE_CONNECT_URL);

			#endif
		}

		/// <summary>
		/// Returns true if the Bose Connect application is available, otherwise false.
		/// </summary>
		/// <returns></returns>
		public static bool IsBoseConnectAvailable()
		{
			bool result;

			#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
			result = true;
			#else
			result = false;
			#endif

			return result;
		}
	}
}
