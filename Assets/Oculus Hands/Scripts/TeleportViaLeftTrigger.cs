using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class TeleportViaLeftTrigger : MonoBehaviour
{
    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] XRInteractorLineVisual visualRay;

    static List<InputDevice> devices = new List<InputDevice>(); // XR Cihazlarýnýn Listesi.

    InputDeviceCharacteristics characteristics;

    void Update()
    {
        characteristics = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left;

        if (devices.Count == 0)
        {
            InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        }

        if (devices.Count > 0)
        {
            var device = devices[0];

            if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                if (triggerValue > 0.1f)
                {
                    EnableInteraction();
                }
                else
                {
                    DisableInteraction();
                }
            }
        }
    }

    void EnableInteraction()
    {
        rayInteractor.enabled = true;
        visualRay.enabled = true;
    }

    void DisableInteraction()
    {
        rayInteractor.enabled = false;
        visualRay.enabled = false;
    }
}
