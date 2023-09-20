using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OrientTorch : XRGrabInteractable
{
    public GameObject TorchAttachPoint;
    private string firstInteractorName = "";

    public void RotateXBy(float angleX)
    {
        transform.rotation = Quaternion.Euler(angleX, 0, 0);
    }

    public void RotateYBy(float angleY)
    {
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }

    public void RotateZBy(float angleZ)
    {
        transform.rotation = Quaternion.Euler(0, 0 , angleZ);
    }

    public void RotateAllBy (float angleX, float angleY, float angleZ)
    {
        transform.rotation = Quaternion.Euler(angleX, angleY, angleZ);
    }

    public void setFirstInteractor()
    {
        // Get the first interactor and set to the firstInteractorName var, as indicated by first element in list:
        var hoverInteractable = GetComponent<XRGrabInteractable>().interactorsHovering[0] as XRBaseInteractor;
        firstInteractorName = hoverInteractable.ToString();
    }

    public void setAttachXPositionBasedOnHand(float posX)
    {
        float magPosX = Mathf.Abs(posX);

        // Depending on the hand that hovered over the torch first, configure torch's position to that hand:
        if (firstInteractorName.Equals("Left Hand Remote Grab (UnityEngine.XR.Interaction.Toolkit.XRRayInteractor)"))
        {
            // From Inspector's view, we must use local to properly adjust within VR hand:
            TorchAttachPoint.transform.localPosition = new Vector3(magPosX, 0f, 0.45f);
        }
        else
        {
            // From Inspector's view, we must use local to properly adjust within VR hand:
            TorchAttachPoint.transform.localPosition = new Vector3(-magPosX, 0f, 0.45f);
        }
    }
}
