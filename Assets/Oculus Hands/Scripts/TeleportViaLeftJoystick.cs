using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportViaLeftJoystick : MonoBehaviour
{
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private XRInteractorLineVisual visualRay;
    [SerializeField] private TeleportationProvider teleportationProvider;

    private static List<InputDevice> devices = new List<InputDevice>(); // XR Cihazlarýnýn Listesi.

    private InputDeviceCharacteristics characteristics;

    private bool canTeleport = false;

    private void Update()
    {
        // Aygýtlarý güncelle
        characteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left;

        if (devices.Count == 0)
        {
            InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        }

        // Iþýnlanma durumunu kontrol et
        if (devices.Count > 0)
        {
            var device = devices[0];

            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickPosition)) // Sol joystick butonu
            {
                float yAxis = joystickPosition.y;

                if (yAxis > 0.1f)
                {
                    EnableInteraction();
                    canTeleport = true; // Iþýnlanma için tetikleme
                }
                else
                {
                    DisableInteraction();
                    canTeleport = false; // Iþýnlanma için tetikleme iptali
                }
            }
        }

        // Iþýnlanma yap
        if (!canTeleport && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Teleportable"))
            {
                TeleportRequest teleportRequest = new TeleportRequest()
                {
                    destinationPosition = hit.point
                };

                teleportationProvider.QueueTeleportRequest(teleportRequest);

                canTeleport = false;
                DisableInteraction();
            }
        }
    }

    public void EnableInteraction()
    {
        rayInteractor.enabled = true;
        visualRay.enabled = true;
    }

    public void DisableInteraction()
    {
        rayInteractor.enabled = false;
        visualRay.enabled = false;
    }
}