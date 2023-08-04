using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Responsible for getting the velocity container from the controller to the climbing provider so it can be processed.
/// </summary>
public class ClimbAnchor : XRBaseInteractable
{
    [SerializeField] private ClimbingProvider climbingProvider;

    protected override void Awake()
    {
        base.Awake();
        FindClimbingProvider();
    }

    private void FindClimbingProvider()
    {
        if (!climbingProvider)
            climbingProvider = FindObjectOfType<ClimbingProvider>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        TryAdd(args.interactorObject);
    }

    private void TryAdd(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingProvider.AddProvider(container);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        TryRemove(args.interactorObject);
    }

    private void TryRemove(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingProvider.RemoveProvider(container);
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor) && interactor is XRDirectInteractor;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && interactor is XRDirectInteractor;
    }
}
