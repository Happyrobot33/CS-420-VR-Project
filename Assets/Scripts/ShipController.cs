using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //1 for forward, 0 for neutral, -1 for reverse
    public int moveState;

    //movement variables
    public float Power = 5;
    public float MaxSpeed = 10f;
    public float turnDampening = 250;

    //component references needed for moving the ship
    protected Rigidbody Rigidbody;
    public Transform TransformOfSelf;

    //For getting the current angle of the steering wheel
    public SteeringWheelController SteeringWheelController;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveState = 0; //default starting value
    }

    private void FixedUpdate()
    {
        //Moving the ship forward or backward
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

        //Turning the ship
        float currentAngle = SteeringWheelController.currentAngle;
        Rigidbody.MoveRotation(Quaternion.RotateTowards(TransformOfSelf.rotation, Quaternion.Euler(0, currentAngle, 0), Time.deltaTime * turnDampening));
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
