using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedGrabData : MonoBehaviour
{
    private static int grabbedInRightHand;
    private static int grabbedInLeftHand;

    // Start is called before the first frame update
    void Start()
    {
        grabbedInRightHand = 0;
        grabbedInLeftHand = 0;
    }

    public static void anObjectIsGrabbedInRightHand()
    {
        grabbedInRightHand++;
    }

    public static void anObjectIsGrabbedInLeftHand()
    {
        grabbedInLeftHand++;
    }

    public static void anObjectIsReleasedInRightHand()
    {
        if (grabbedInRightHand > 0)
        {
            // Only allow grabbed to be decremented down to 0:
            grabbedInRightHand--;
        }
    }

    public static void anObjectIsReleasedInLeftHand()
    {
        if (grabbedInLeftHand > 0)
        {
            // Only allow grabbed to be decremented down to 0:
            grabbedInLeftHand--;
        }
    }

    public static bool isSomethingGrabbedInRightHand()
    {
        if (grabbedInRightHand > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool isSomethingGrabbedInLeftHand()
    {
        if (grabbedInLeftHand > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
