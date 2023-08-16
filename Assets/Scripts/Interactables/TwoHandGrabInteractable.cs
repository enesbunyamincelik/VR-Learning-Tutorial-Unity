using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable 
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    public enum TwoHandRotationType { None, First, Second  };
    public TwoHandRotationType twoHandRotationType;
    public bool snapToSeconHand = true;

    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    private Quaternion initialRotationOffset;

    [System.Obsolete]
    void Start()
    {
        foreach (var item in secondHandGrabPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrabbed);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }

    [System.Obsolete]
#pragma warning disable CS0809 // Eski üye eski olmayan üyeyi geçersiz kýlar
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
#pragma warning restore CS0809 // Eski üye eski olmayan üyeyi geçersiz kýlar
    {
        if (secondInteractor && selectingInteractor)
        {
            // Computes the rotation
            if (snapToSeconHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            else
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
        }
        base.ProcessInteractable(updatePhase);
    }

    /// <summary>
    /// Gets the rotation types of two hands and controls them
    /// None: No interactors can rotate the interactable object
    /// First: Only first hand can rotate the interactable object
    /// Second: Only second hand can rotate the interactable object
    /// The `LookRotation` method calculates a rotation that will orient towards a specified direction or point.
    /// </summary>

    [System.Obsolete]
    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position -  selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
        }

        return targetRotation;
    }

    [System.Obsolete]
    public void OnSecondHandGrabbed(XRBaseInteractor interactor)
    {
        Debug.Log("SECOND HAND GRABBED");
        secondInteractor = interactor;
        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        Debug.Log("SECOND HAND RELEASE");
        secondInteractor = null;
    }
    
    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("FIRST HAND GRABBED");
        base.OnSelectEntered(interactor);
        attachInitialRotation = interactor.attachTransform.localRotation;
    }

    [System.Obsolete]
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("FIRST HAND RELEASE");
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachInitialRotation;
    }

    [System.Obsolete]
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}