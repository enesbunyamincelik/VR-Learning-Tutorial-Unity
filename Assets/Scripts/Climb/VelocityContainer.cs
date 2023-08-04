using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Instead of creating a custom controller, we add a simple component to hold the action.
/// </summary>
public class VelocityContainer : MonoBehaviour
{
    [SerializeField] private InputActionProperty velocityInput;

    public Vector3 Velocity => velocityInput.action.ReadValue<Vector3>();
}
