using UnityEngine;
using UnityEngine.UI;

namespace Bose.Wearable
{
	/// <summary>
	/// Shown when a required permission is not granted by the user.
	/// </summary>
	internal sealed class PermissionRequestedConnectDisplay : WearableConnectDisplayBase
	{
		[Header("UI Refs")]
		[SerializeField]
		private Button _grantPermissionButton;

		[SerializeField]
		private Button _continueWithoutButton;

		[SerializeField]
		private Text _headerText;

		[SerializeField]
		private Text _warningText;

		private OSPermission _requiredPermission;

		private const string REQUIRED_PERMISSION_TITLE_FORMAT = "{0} Permission Required";

		protected override void Awake()
		{
			base.Awake();

			_panel.OSPermissionRequired += OnOSPermissionRequired;
			_panel.OSServiceRequired += OnOSServiceRequired;
			_panel.DeviceSearching += Hide;
			_panel.DeviceConnectFailure += Hide;
			_panel.DeviceDisconnected += OnDeviceDisconnected;
			_panel.OSPermissionFailure += OnOSPermissionFailure;
			_panel.OSServiceFailure += OnOSServiceFailure;

			_grantPermissionButton.onClick.AddListener(OnGrantPermissionClicked);
			_continueWithoutButton.onClick.AddListener(OnContinueWithoutBoseARClicked);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_panel.OSPermissionRequired -= OnOSPermissionRequired;
			_panel.OSServiceRequired -= OnOSServiceRequired;
			_panel.DeviceSearching -= Hide;
			_panel.DeviceConnectFailure -= Hide;
			_panel.DeviceDisconnected -= OnDeviceDisconnected;
			_panel.OSPermissionFailure -= OnOSPermissionFailure;
			_panel.OSServiceFailure -= OnOSServiceFailure;

			_grantPermissionButton.onClick.RemoveListener(OnGrantPermissionClicked);
			_continueWithoutButton.onClick.RemoveListener(OnContinueWithoutBoseARClicked);
		}

		private void OnOSServiceRequired(OSService service)
		{
			Hide();
		}

		private void OnOSPermissionRequired(OSPermission permission)
		{
			_requiredPermission = permission;
			_messageText.text = string.Empty;
			_headerText.text = string.Format(REQUIRED_PERMISSION_TITLE_FORMAT, permission);
			_warningText.text = _wearableControl.GetPermissionRequiredText(permission);

			Show();
		}

		private void OnDeviceDisconnected(Device obj)
		{
			Hide();
		}

		private void OnOSServiceFailure(OSService service)
		{
			Hide();
		}

		private void OnOSPermissionFailure(OSPermission permission)
		{
			Hide();
		}

		private void OnGrantPermissionClicked()
		{
			_wearableControl.RequestPermission(_requiredPermission);

			Hide();
		}

		private void OnContinueWithoutBoseARClicked()
		{
			_wearableControl.DenyPermissionOrService();
		}
	}
}
