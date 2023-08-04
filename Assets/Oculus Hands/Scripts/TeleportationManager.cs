using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;

    private InputAction _thumbstick;
    private bool _isActive;

    void Start()
    {
        rayInteractor.enabled = false;

        var activate = inputActions.FindActionMap("XRI Lefthand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = inputActions.FindActionMap("XRI Lefthand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        activate.performed += OnTeleportCancel;

        var _thumbstick = inputActions.FindActionMap("XRI Lefthand Locomotion").FindAction("Move");
        _thumbstick.Enable();
    }

    void Update()
    {
        if (!_isActive)
            return;

        if (_thumbstick.triggered)
            return;

        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        provider.QueueTeleportRequest(teleportRequest);
    }


    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
    }
}
