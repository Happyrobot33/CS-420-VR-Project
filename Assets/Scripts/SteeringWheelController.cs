using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    //Right Hand
    public GameObject rightHand;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;

    //Left Hand
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snapPositions;

    //Objects to control
    public GameObject Ship;
    private Rigidbody ShipRigidBody;

    public float currentSteeringWheelRotation = 0;

    //turn dampening, lower number makes the ship take longer to reach the target rotation
    //for the ship to just copy the steering wheel movement, use a high number like 9999
    private float turnDampening = 250;

    public Transform directionalObject;

    // Start is called before the first frame update
    void Start()
    {
        ShipRigidBody = Ship.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseHandsFromWheel();

        ConvertHandRotationToSteeringWheelRotation();

        TurnShip();

        currentSteeringWheelRotation = -transform.rotation.eulerAngles.z;
    }

    private void TurnShip()
    {
        //Turns Ship compared to the steering wheel
        var turn = -transform.rotation.eulerAngles.z;
        if(turn < -350)
        {
            turn = turn + 360;
        }
        ShipRigidBody.MoveRotation(Quaternion.RotateTowards(Ship.transform.rotation, Quaternion.Euler(0, turn, 0), Time.deltaTime * turnDampening));
    }

    private void ConvertHandRotationToSteeringWheelRotation()
    {
        if(rightHandOnWheel == true && leftHandOnWheel == false)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else if(rightHandOnWheel == false && leftHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else if(rightHandOnWheel == true && leftHandOnWheel == true)
        {
            Quaternion newRotLeft = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion newRotRight = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(newRotLeft, newRotRight, 1f / 2f);
            directionalObject.rotation = finalRot;
            transform.parent = directionalObject;
        }
    }

    private void ReleaseHandsFromWheel()
    {
        if(rightHandOnWheel == true && ) //get button for grabbable interaction
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHand.transform.rotation = rightHandOriginalParent.rotation;
            rightHandOnWheel = false;
            Debug.Log("Right hand released the wheel");
        }

        if(leftHandOnWheel == true && ) //get button for grabbable interaction
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHand.transform.rotation = leftHandOriginalParent.rotation;
            leftHandOnWheel = false;
            Debug.Log("Left hand released the wheel;");
        }

        if(leftHandOnWheel == false && rightHandOnWheel == false)
        {
            Debug.Log("No hands are on the wheel");
            //reset steering wheel to not be parent of directional object if wheel is released
            transform.parent = Ship.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("PlayerHand"))
        {
            if(rightHandOnWheel == false && ) //get button for grabbable interaction
            {
                Debug.Log("Right hand placed on the wheel");
                PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
            }

            if(leftHandOnWheel == false && ) //get button for grabbable interaction
            {
                PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
            }
        }
    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        //Set variables to the first snap position in array
        var shortestDistance = Vector3.Distance(snapPositions[0].position, hand.transform.position);
        var bestSnap = snapPositions[0];
        //loop through all snap positions
        foreach(var snapPosition in snapPositions)
        {
            //if no hand is child of this snap position
            if(snapPosition.childCount == 0)
            {
                //distance between hand and snap position
                var distance = Vector3.Distance(snapPosition.position, hand.transform.position);
                //if distance is shorter than current shortest distance
                if(distance < shortestDistance)
                {
                    //set this distance to the shortest distance and this snap to the bestSnap
                    shortestDistance = distance;
                    bestSnap = snapPosition;
                }
            }
        }
        //we need XHandOriginalParent to be able to reset the hand after release
        originalParent = hand.transform.parent;

        //set best snap as parent and hand position to snap position
        hand.transform.parent = bestSnap.transform;
        hand.transform.position = bestSnap.transform.position;

        handOnWheel = true;
    }
}
