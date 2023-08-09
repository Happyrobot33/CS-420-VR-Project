using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int moveState;
    public Transform SteeringWheel;

    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    protected Rigidbody Rigidbody;
    protected Quaternion StartRotation;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        StartRotation = Motor.localRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveState = 0;
    }

    private void FixedUpdate()
    {
        var forceDiretion = transform.forward;
        var steer = 0;

        if(SteeringWheel.rotation.eulerAngles.z < 1)
        {
            Debug.Log("Turning Right");
            //turn right
            steer = -1;
        }
        if(SteeringWheel.rotation.eulerAngles.z > 1)
        {
            Debug.Log("Turning Left");
            //turn left
            steer = 1;
        }

        //find a way to implement variable speed based on how far the wheel has been turned
        //maybe change SteerPower from being a static number to an adjustable variable
        Rigidbody.AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);

        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        if(moveState == 1)
        {
            //move forward
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed, Power);
        }
        if( moveState == -1)
        {
            //move backward
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * -MaxSpeed, Power);
        }
    }

    public void Forward()
    {
        moveState = 1;
    }

    public void Reverse()
    {
        moveState = -1;
    }

    public void Stop()
    {
        moveState = 0;
    }
}
