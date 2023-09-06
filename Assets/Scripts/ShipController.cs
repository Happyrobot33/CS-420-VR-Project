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
    private Rigidbody Rigidbody;

    //For getting the current angle of the steering wheel
    private SteeringWheelController SteeringWheelController;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SteeringWheelController = GameObject.FindAnyObjectByType<SteeringWheelController>();
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
            Rigidbody.AddForce(0, 0, Power);
            if (Rigidbody.velocity.magnitude > MaxSpeed)
            {
                Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, MaxSpeed);
            }
        }
        if( moveState == -1)
        {
            //move backward
            Rigidbody.AddForce(0, 0, -Power);
            if (Rigidbody.velocity.magnitude < -MaxSpeed)
            {
                Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, -MaxSpeed);
            }
        }
        if(moveState != 0) //The ship must be moving to turn
        {
            //Turning the ship
            float currentAngle = SteeringWheelController.currentAngle;
            Rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, currentAngle, 0), Time.deltaTime * turnDampening));
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
