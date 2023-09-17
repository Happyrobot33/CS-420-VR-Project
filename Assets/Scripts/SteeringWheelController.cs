using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheelController : XRBaseInteractable
{
    [SerializeField] private Transform steeringWheel;

    public UnityEvent<float> OnWheelRotated;

    public float currentAngle { get; protected set; } = 0.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentAngle = FindWheelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        //Convert that direction to an angle, then rotate
        float totalAngle = FindWheelAngle();

        //Apply difference in angle to wheel
        float angleDifference = currentAngle - totalAngle;
        steeringWheel.Rotate(transform.forward, -angleDifference, Space.World);

        //Start angle for next process
        currentAngle = totalAngle;
        OnWheelRotated.Invoke(angleDifference);
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        //Combine direction of current interactors
        foreach(IXRActivateInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector2 position)
    {
        //Convert the hand position to local, so we can find the angle easier
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        //Use a consistent forward direction to find the angle
        return Vector2.SignedAngle(transform.up, direction);
    }

    private float FindRotationSensitivity()
    {
        //Use a smaller rotation sensitivity with two hands
        return 1.0f / interactorsSelecting.Count;
    }
}
