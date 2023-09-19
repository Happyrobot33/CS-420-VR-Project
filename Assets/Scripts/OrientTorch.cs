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
        var hoverInteractable = GetComponent<XRGrabInteractable>().interactorsHovering[0] as XRBaseInteractor;
        firstInteractorName = hoverInteractable.ToString();
    }

    public void setAttachXPositionBasedOnHand(float posX)
    {
        //var grabInteractable = GetComponent<XRGrabInteractable>().interactorsSelecting[0] as XRBaseInteractor;
        //string toStrInteractor = grabInteractable.ToString();
        float magPosX = Mathf.Abs(posX);

        //Debug.Log("Selected Interactable's Interactor toString name: " + toStrInteractor);
        //if (toStrInteractor.Equals("Left Hand Remote Grab (UnityEngine.XR.Interaction.Toolkit.XRRayInteractor)"))
        if (firstInteractorName.Equals("Left Hand Remote Grab (UnityEngine.XR.Interaction.Toolkit.XRRayInteractor)"))
        {
            //TorchAttachPoint.transform.position = new Vector3(0.2f, 0f, 0.45f);
            Debug.Log("The attach point's LOCAL position is: " + TorchAttachPoint.transform.localPosition);
            Debug.Log("To be more specific, it's LOCAL X is: " + TorchAttachPoint.transform.localPosition.x);
            TorchAttachPoint.transform.localPosition = new Vector3(magPosX, 0f, 0.45f);
            //Debug.Log("The NEW attach point's position is: " + TorchAttachPoint.transform.position);
            //Debug.Log("To be more specific, it's NEW X is: " + TorchAttachPoint.transform.position.x);
        }
        else
        {
            //TorchAttachPoint.transform.position = new Vector3(-0.2f, 0f, 0.45f);
            Debug.Log("The attach point's position is: " + TorchAttachPoint.transform.position);
            Debug.Log("To be more specific, it's X is: " + TorchAttachPoint.transform.position.x);
            //Debug.Log(toStrInteractor);
            TorchAttachPoint.transform.localPosition = new Vector3(-magPosX, 0f, 0.45f);
            //Debug.Log("The NEW attach point's position is: " + TorchAttachPoint.transform.position);
            //Debug.Log("To be more specific, it's NEW X is: " + TorchAttachPoint.transform.position.x);
        }
    }
}
