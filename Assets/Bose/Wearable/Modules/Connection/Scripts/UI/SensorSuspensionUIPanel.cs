using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bose.Wearable
{
	[RequireComponent(typeof(Canvas))]
	public sealed class SensorSuspensionUIPanel : Singleton<SensorSuspensionUIPanel>
	{
		/// <summary>
		/// The Canvas on the root UI element.
		/// </summary>
		[Header("UI Refs")]
		[SerializeField]
		private Canvas _canvas;

		/// <summary>
		/// The CanvasGroup on the root UI element of this Canvas.
		/// </summary>
		[SerializeField]
		private CanvasGroup _canvasGroup;

		[SerializeField]
		private Button _launchExternalAppButton;

		[SerializeField]
		private Button _continueWithoutBoseButton;

		[SerializeField]
		private Text _warningText;

		private WearableControl _wearableControl;
		private SensorServiceSuspendedReason _reason;

		private const string VOICE_ASSISTANT_WARNING
			= "Bose AR is disabled when you are using your voice assistant. Your AR experience should " +
			  "resume shortly.";

		private const string MULTI_POINT_WARNING
			= "We noticed that your QC35 II headphones are connected to another device. To get the full " +
			  "Bose AR experience, you'll have to disconnect from the other device.";

		private const string MUSIC_SHARING_WARNING
			= "To experience Bose AR, your AR-enabled product cannot be sharing music with another product. " +
			  "End Music Share to continue using Bose AR.";

		private const string UNKNOWN_REASON_WARNING
			= "Bose AR is disabled temporarily when you are using a voice assistant or indefinitely while you are " +
			  "connected to a secondary device through Music Share or multi-point connectivity.";

		protected override void Awake()
		{
			base.Awake();

			_wearableControl = WearableControl.Instance;
			_wearableControl.DeviceConnected += OnDeviceConnected;
			_wearableControl.DeviceDisconnected += OnDeviceDisconnected;
			_wearableControl.SensorServiceResumed += OnSensorServiceResumed;
			_wearableControl.SensorServiceSuspended += OnSensorServiceSuspended;

			_launchExternalAppButton.onClick.AddListener(LaunchExternalApp);
			_continueWithoutBoseButton.onClick.AddListener(ContinueWithoutBoseAR);

			Hide();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_wearableControl.DeviceConnected -= OnDeviceConnected;
			_wearableControl.DeviceDisconnected -= OnDeviceDisconnected;
			_wearableControl.SensorServiceResumed -= OnSensorServiceResumed;
			_wearableControl.SensorServiceSuspended -= OnSensorServiceSuspended;

			_launchExternalAppButton.onClick.RemoveListener(LaunchExternalApp);
			_continueWithoutBoseButton.onClick.RemoveListener(ContinueWithoutBoseAR);
		}

		private void Show()
		{
			_canvas.enabled = true;
			_canvasGroup.alpha = 1;
			_canvasGroup.interactable = true;

			// We allow the user to enter Bose Connect if we don't know for certain the user is currently in a
			// VPA suspension. Since VPA suspensions resolve on their own and often in short order, it would be
			// a worse experience for a user to enter Bose Connect in an attempt to resolve that suspension.
			bool allowBoseConnect = _reason != SensorServiceSuspendedReason.VoiceAssistantInUse &&
			                        PlatformTools.IsBoseConnectAvailable();

			// If we know for certain that the user is in music sharing or multipoint, then we allow them
			// to continue without Bose AR, effectively killing their Bose AR connection.
			bool allowContinueWithout = (_reason == SensorServiceSuspendedReason.MusicSharingActive ||
			                            _reason == SensorServiceSuspendedReason.MultipointConnectionActive);

			_launchExternalAppButton.gameObject.SetActive(allowBoseConnect);
			_continueWithoutBoseButton.gameObject.SetActive(allowContinueWithout);

			switch (_reason)
			{
				case SensorServiceSuspendedReason.MusicSharingActive:
					_warningText.text = MUSIC_SHARING_WARNING;
					break;
				case SensorServiceSuspendedReason.MultipointConnectionActive:
					_warningText.text = MULTI_POINT_WARNING;
					break;
				case SensorServiceSuspendedReason.VoiceAssistantInUse:
					_warningText.text = VOICE_ASSISTANT_WARNING;
					break;
				case SensorServiceSuspendedReason.UnknownReason:
					_warningText.text = UNKNOWN_REASON_WARNING;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Hide()
		{
			_canvas.enabled = false;
			_canvasGroup.alpha = 0;
			_canvasGroup.interactable = false;
		}

		private void LaunchExternalApp()
		{
			PlatformTools.LaunchBoseConnectApp();
		}

		private void ContinueWithoutBoseAR()
		{
			_wearableControl.DisconnectFromDevice();
		}

		private void OnDeviceConnected(Device device)
		{
			if (device.deviceStatus.ServiceSuspended)
			{
				_reason = device.deviceStatus.GetServiceSuspendedReason();

				Show();
			}
		}

		private void OnDeviceDisconnected(Device device)
		{
			Hide();
		}

		private void OnSensorServiceResumed()
		{
			Hide();
		}

		private void OnSensorServiceSuspended(SensorServiceSuspendedReason reason)
		{
			_reason = reason;

			Show();
		}
	}
}
