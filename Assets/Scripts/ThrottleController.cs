using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleController : MonoBehaviour
{
    private ShipController ShipController;
    public float angle;

    private void Awake()
    {
        ShipController = GameObject.FindAnyObjectByType<ShipController>();
        angle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle = transform.rotation.eulerAngles.z;
        ThrottleCheck(angle);
    }

    public void ThrottleCheck(float angle)
    {
        if(angle >= 10 && angle <= 15)
        {
            //set to forward
            ShipController.Forward();
        }
        else if(angle <= 350 && angle >= 345) // must be 350 instead of -10 because of how unity calculates angles at runtime
        {
            //set to reverse
            ShipController.Reverse();
        }
        else
        {
            //set to neutral
            ShipController.Stop();
        }
    }
}
