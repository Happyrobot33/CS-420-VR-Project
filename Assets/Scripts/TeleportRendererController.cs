using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportRendererController : MonoBehaviour
{
    //public GameObject LeftTeleportController;
    //public GameObject RightTeleportController;
    public GameObject LeftRemoteGrabController;
    public GameObject RightRemoteGrabController;
    public GameObject LeftGrabController;
    public GameObject RightGrabController;
    /*public void disableHoveringTeleportVisual()
    {
        int numOfInteractorsHovering = GetComponent<XRGrabInteractable>().interactorsHovering.Count;
        for (int index = 0; index < numOfInteractorsHovering; index++)
        {
            var currHoverInteractor = GetComponent<XRGrabInteractable>().interactorsHovering[index] as XRBaseInteractor;
            string interactorsName = currHoverInteractor.ToString();

            if (interactorsName.Contains("Left"))
            {
                LeftTeleportController.SetActive(false);
            }
            if (interactorsName.Contains("Right"))
            {
                RightTeleportController.SetActive(false);
            }
        }
    }*/

    /*public void enableHoveringTeleportVisual()
    {
        int numOfInteractorsHovering = GetComponent<XRGrabInteractable>().interactorsHovering.Count;
        // If one of the interactors is still hovering, decide which one to re-enable:
        if (numOfInteractorsHovering > 0)
        {
            for (int index = 0; index < numOfInteractorsHovering; index++)
            {
                var currHoverInteractor = GetComponent<XRGrabInteractable>().interactorsHovering[index] as XRBaseInteractor;
                string interactorsName = currHoverInteractor.ToString();

                // There can only either be 1 interactor left after this exited hover call, or 0 left:
                if (interactorsName.Contains("Left"))
                {
                    RightTeleportController.SetActive(true);
                }
                if (interactorsName.Contains("Right"))
                {
                    LeftTeleportController.SetActive(true);
                }
            }
        }
        // Else, if none are hovering anymore, re-enable them all:
        else
        {
            LeftTeleportController.SetActive(true);
            RightTeleportController.SetActive(true);
        }
    }*/

    //Called when a non-teleportation object is selected by an interactor:
    /*public void disableSelectingTeleportVisual()
    {
        int numOfInteractorsSelecting = GetComponent<XRGrabInteractable>().interactorsSelecting.Count;
        for (int index = 0; index < numOfInteractorsSelecting; index++)
        {
            var currSelectInteractor = GetComponent<XRGrabInteractable>().interactorsSelecting[index] as XRBaseInteractor;
            string interactorsName = currSelectInteractor.ToString();

            if (interactorsName.Contains("Left"))
            {
                LeftTeleportController.SetActive(false);
                SharedGrabData.anObjectIsGrabbedInLeftHand();
            }
            if (interactorsName.Contains("Right"))
            {
                RightTeleportController.SetActive(false);
                SharedGrabData.anObjectIsGrabbedInRightHand();
            }
        }
    }*/

    //Called when a non-teleportation object is de-selected by an interactor:
    /*public void enableSelectingTeleportVisual()
    {
        int numOfInteractorsSelecting = GetComponent<XRGrabInteractable>().interactorsSelecting.Count;
        // If one of the interactors is still selecting, decide which one to re-enable:
        if (numOfInteractorsSelecting > 0)
        {
            for (int index = 0; index < numOfInteractorsSelecting; index++)
            {
                var currSelectInteractor = GetComponent<XRGrabInteractable>().interactorsSelecting[index] as XRBaseInteractor;
                string interactorsName = currSelectInteractor.ToString();

                // There can only either be 1 interactor left after this exited select call, or 0 left:
                if (interactorsName.Contains("Left"))
                {
                    RightTeleportController.SetActive(true);
                    SharedGrabData.anObjectIsReleasedInLeftHand();
                }
                if (interactorsName.Contains("Right"))
                {
                    LeftTeleportController.SetActive(true);
                    SharedGrabData.anObjectIsReleasedInRightHand();
                }
            }
        }
        // Else, if none are selecting anymore, re-enable them all:
        else
        {
            LeftTeleportController.SetActive(true);
            RightTeleportController.SetActive(true);
            SharedGrabData.anObjectIsReleasedInLeftHand();
            SharedGrabData.anObjectIsReleasedInRightHand();
        }
    }*/

    //Called when a teleportation object is hovered by an interactor:
    /*public void disableHoveringGrabbableVisual()
    {
        int numOfInteractorsHovering = GetComponent<BaseTeleportationInteractable>().interactorsHovering.Count;
        for (int index = 0; index < numOfInteractorsHovering; index++)
        {
            var currHoverInteractor = GetComponent<BaseTeleportationInteractable>().interactorsHovering[index] as XRBaseInteractor;
            string interactorsName = currHoverInteractor.ToString();

            if (interactorsName.Contains("Left") && (SharedGrabData.isSomethingGrabbedInLeftHand() == false))
            {
                LeftRemoteGrabController.SetActive(false);
            }
            if (interactorsName.Contains("Right") && (SharedGrabData.isSomethingGrabbedInRightHand() == false))
            {
                RightRemoteGrabController.SetActive(false);
            }
        }
    }*/

    //Called when a teleportation object is no longer hovered by an interactor:
    /*public void enableHoveringGrabbableVisual()
    {
        int numOfInteractorsHovering = GetComponent<BaseTeleportationInteractable>().interactorsHovering.Count;
        // If one of the interactors is still hovering, decide which one to re-enable:
        if (numOfInteractorsHovering > 0)
        {
            for (int index = 0; index < numOfInteractorsHovering; index++)
            {
                var currHoverInteractor = GetComponent<BaseTeleportationInteractable>().interactorsHovering[index] as XRBaseInteractor;
                string interactorsName = currHoverInteractor.ToString();

                // There can only either be 1 interactor left after this exited hover call, or 0 left:
                if (interactorsName.Contains("Left"))
                {
                    RightRemoteGrabController.SetActive(true);
                }
                if (interactorsName.Contains("Right"))
                {
                    LeftRemoteGrabController.SetActive(true);
                }
            }

        }
        // Else, if none are hovering anymore, re-enable them all:
        else
        {
            LeftRemoteGrabController.SetActive(true);
            RightRemoteGrabController.SetActive(true);
        }
    }*/

    /*public void disableSelectingGrabbableVisual()
    {

        int numOfInteractorsSelecting = GetComponent<BaseTeleportationInteractable>().interactorsSelecting.Count;
        for (int index = 0; index < numOfInteractorsSelecting; index++)
        {
            var currSelectInteractor = GetComponent<BaseTeleportationInteractable>().interactorsSelecting[index] as XRBaseInteractor;
            string interactorsName = currSelectInteractor.ToString();

            if (interactorsName.Contains("Left"))
            {
                LeftRemoteGrabController.SetActive(false);
            }
            if (interactorsName.Contains("Right"))
            {
                RightRemoteGrabController.SetActive(false);
            }
        }
    }*/

    /*public void enableSelectingGrabbableVisual()
    {
        int numOfInteractorsSelecting = GetComponent<BaseTeleportationInteractable>().interactorsSelecting.Count;
        // If one of the interactors is still selecting, decide which one to re-enable:
        if (numOfInteractorsSelecting > 0)
        {
            for (int index = 0; index < numOfInteractorsSelecting; index++)
            {
                var currSelectInteractor = GetComponent<BaseTeleportationInteractable>().interactorsSelecting[index] as XRBaseInteractor;
                string interactorsName = currSelectInteractor.ToString();

                // There can only either be 1 interactor left after this exited select call, or 0 left:
                if (interactorsName.Contains("Left"))
                {
                    RightRemoteGrabController.SetActive(true);
                }
                if (interactorsName.Contains("Right"))
                {
                    LeftRemoteGrabController.SetActive(true);
                }
            }
        }
        // Else, if none are selecting anymore, re-enable them all:
        else
        {
            LeftRemoteGrabController.SetActive(true);
            RightRemoteGrabController.SetActive(true);
        }
    }*/

    //Called when a non-teleportation object is selected by an interactor:
    public void disableSelectingRemoteGrabVisual()
    {
        int numOfInteractorsSelecting = GetComponent<XRGrabInteractable>().interactorsSelecting.Count;
        for (int index = 0; index < numOfInteractorsSelecting; index++)
        {
            var currSelectInteractor = GetComponent<XRGrabInteractable>().interactorsSelecting[index] as XRBaseInteractor;
            string interactorsName = currSelectInteractor.ToString();

            if (interactorsName.Contains("Left Hand Grab"))
            {
                LeftRemoteGrabController.SetActive(false);
                SharedGrabData.anObjectIsGrabbedInLeftHand();
            }
            if (interactorsName.Contains("Right Hand Grab"))
            {
                RightRemoteGrabController.SetActive(false);
                SharedGrabData.anObjectIsGrabbedInRightHand();
            }
        }
    }

    //Called when a non-teleportation object is de-selected by an interactor:
    public void enableSelectingRemoteGrabVisual()
    {
        int numOfInteractorsSelecting = GetComponent<XRGrabInteractable>().interactorsSelecting.Count;
        // If one of the interactors is still selecting, decide which one to re-enable:
        if (numOfInteractorsSelecting > 0)
        {
            for (int index = 0; index < numOfInteractorsSelecting; index++)
            {
                var currSelectInteractor = GetComponent<XRGrabInteractable>().interactorsSelecting[index] as XRBaseInteractor;
                string interactorsName = currSelectInteractor.ToString();

                // There can only either be 1 interactor left after this exited select call, or 0 left:
                if (interactorsName.Contains("Left Hand Grab"))
                {
                    RightRemoteGrabController.SetActive(true);
                    SharedGrabData.anObjectIsReleasedInLeftHand();
                }
                if (interactorsName.Contains("Right Hand Grab"))
                {
                    LeftRemoteGrabController.SetActive(true);
                    SharedGrabData.anObjectIsReleasedInRightHand();
                }
            }
        }
        else
        {
            
            RightRemoteGrabController.SetActive(true);
            LeftRemoteGrabController.SetActive(true);
            SharedGrabData.anObjectIsReleasedInLeftHand();
            SharedGrabData.anObjectIsReleasedInRightHand();
        }

    }
}
