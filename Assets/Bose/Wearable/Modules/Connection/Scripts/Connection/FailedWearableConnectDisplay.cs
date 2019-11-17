using UnityEngine;
using UnityEngine.UI;

namespace Bose.Wearable
{
	/// <summary>
	/// Shown when a device connection attempt has failed
	/// </summary>
	internal sealed class FailedWearableConnectDisplay : WearableConnectDisplayBase
	{
		[SerializeField]
		private Button _searchButton;

		[Header("Sound Clips")]
		[SerializeField]
		private AudioClip _sfxConnectFailed;

		private const string PERMISSIONS_FAILURE_MESSAGE_FORMAT = "Searching for devices failed. {0} permissions are required.";
		private const string SERVICE_FAILURE_MESSAGE_FORMAT = "Searching for devices failed. {0} is required to be enabled.";

		protected override void Awake()
		{
			SetupAudio();

			base.Awake();
		}

		private void OnEnable()
		{
			_panel.DeviceConnectFailure += OnDeviceConnectFailure;
			_panel.OSPermissionFailure += OnOSPermissionFailure;
			_panel.OSServiceFailure += OnOSServiceFailure;

			_searchButton.onClick.AddListener(OnSearchButtonClicked);
		}

		private void OnDisable()
		{
			_panel.DeviceConnectFailure -= OnDeviceConnectFailure;
			_panel.OSPermissionFailure -= OnOSPermissionFailure;
			_panel.OSServiceFailure -= OnOSServiceFailure;

			_searchButton.onClick.RemoveAllListeners();
		}

		private void OnDeviceConnectFailure()
		{
			_messageText.text = WearableConstants.DEVICE_CONNECT_FAILURE_MESSAGE;

			Show();
		}

		private void OnOSServiceFailure(OSService service)
		{
			_messageText.text = string.Format(SERVICE_FAILURE_MESSAGE_FORMAT, service.ToString());

			Show();
		}

		private void OnOSPermissionFailure(OSPermission permission)
		{
			_messageText.text = string.Format(PERMISSIONS_FAILURE_MESSAGE_FORMAT, permission.ToString());

			Show();
		}

		private void OnSearchButtonClicked()
		{
			_panel.StartSearch();

			Hide();
		}

		protected override void Show()
		{
			PlayFailureSting();

			_panel.EnableCloseButton();

			base.Show();
		}

		private void PlayFailureSting()
		{
			_audioControl.PlayOneShot(_sfxConnectFailed);
		}
	}
}
