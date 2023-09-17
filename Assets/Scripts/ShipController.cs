using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //1 for forward, 0 for neutral, -1 for reverse
    public int moveState;

    //movement variables
    public float Power = 5;

    //component references needed for moving the ship
    public Rigidbody SteeringWheel;
    public Transform Position;
    public Transform Rotation;

    // Start is called before the first frame update
    void Start()
    {
        moveState = 0; //default starting value
    }

    private void FixedUpdate()
    {
        Rotation.rotation *= Quaternion.Euler(0, Quaternion.Euler(transform.InverseTransformVector(SteeringWheel.angularVelocity) * Time.fixedDeltaTime).eulerAngles.z, 0);
        Position.Translate(Rotation.forward * -moveState * Power * Time.fixedDeltaTime); //moveState must be flipped to make sure the ship moves in the correct direction
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
