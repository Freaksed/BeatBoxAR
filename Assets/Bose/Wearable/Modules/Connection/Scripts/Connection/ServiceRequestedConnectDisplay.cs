using UnityEngine;
using UnityEngine.UI;

namespace Bose.Wearable
{
	/// <summary>
	/// Shown when a required service needs to be enabled.
	/// </summary>
	internal sealed class ServiceRequestedConnectDisplay : WearableConnectDisplayBase
	{
		[Header("UI Refs")]
		[SerializeField]
		private Button _enableServiceButton;

		[SerializeField]
		private Button _continueWithoutButton;

		[SerializeField]
		private Text _headerText;

		[SerializeField]
		private Text _warningText;

		#pragma warning disable 0414

		private OSService _requiredService;

		#pragma warning restore 0414

		private const string REQUIRED_SERVICE_TITLE_FORMAT = "Turn on {0}";

		protected override void Awake()
		{
			base.Awake();

			_panel.OSPermissionRequired += OnOSPermissionRequired;
			_panel.OSServiceRequired += OnOSServiceRequired;
			_panel.DeviceSearching += Hide;
			_panel.DeviceConnectFailure += Hide;
			_panel.DeviceSearching += Hide;
			_panel.OSPermissionFailure += OnOSPermissionFailure;
			_panel.OSServiceFailure += OnOSServiceFailure;

			_enableServiceButton.onClick.AddListener(OnTryAgainClicked);
			_continueWithoutButton.onClick.AddListener(OnContinueWithoutBoseARClicked);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_panel.OSPermissionRequired -= OnOSPermissionRequired;
			_panel.OSServiceRequired -= OnOSServiceRequired;
			_panel.DeviceSearching -= Hide;
			_panel.DeviceConnectFailure -= Hide;
			_panel.DeviceSearching -= Hide;
			_panel.OSPermissionFailure -= OnOSPermissionFailure;
			_panel.OSServiceFailure -= OnOSServiceFailure;

			_enableServiceButton.onClick.RemoveListener(OnTryAgainClicked);
			_continueWithoutButton.onClick.RemoveListener(OnContinueWithoutBoseARClicked);
		}

		private void OnOSPermissionRequired(OSPermission permission)
		{
			Hide();
		}

		private void OnOSServiceRequired(OSService service)
		{
			_requiredService = service;
			_messageText.text = string.Empty;
			_headerText.text = string.Format(REQUIRED_SERVICE_TITLE_FORMAT, service);
			_warningText.text = _wearableControl.GetServiceRequiredText(service);

			Show();
		}

		private void OnTryAgainClicked()
		{
			Hide();

			_panel.StartSearch();
		}

		private void OnOSServiceFailure(OSService service)
		{
			Hide();
		}

		private void OnOSPermissionFailure(OSPermission permission)
		{
			Hide();
		}

		private void OnContinueWithoutBoseARClicked()
		{
			_wearableControl.DenyPermissionOrService();
		}
	}
}
