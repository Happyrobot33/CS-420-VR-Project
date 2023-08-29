using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleController : MonoBehaviour
{
    public Transform TransformOfSelf;
    public ShipController ShipController;

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = TransformOfSelf.rotation.eulerAngles.x;
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
