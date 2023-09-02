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
        if(angle > 30)
        {
            //set to forward
            ShipController.Forward();
        }
        else if(angle < -30)
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
