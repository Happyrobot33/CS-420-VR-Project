using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController: MonoBehaviour
{
    public enum LeverState { Forward, Neutral, Reverse};
    public LeverState leverState;
    private ShipController Ship;
    public GameObject LeverCollider;
    public GameObject LeverPivot;
    public GameObject SteeringWheel;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Ship = FindObjectOfType<ShipController>();
        leverState = LeverState.Neutral;
    }

    // Update is called once per frame
    void Update()
    {
        //Forward
        if(Input.GetKeyDown(KeyCode.W))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == LeverCollider.name)
                {
                    //set the state of the lever one step forward, unless it is already set to Forward

                    if (leverState == LeverState.Reverse) //set to Neutral if in Reverse
                    {
                        LeverPivot.transform.eulerAngles = new Vector3(0, 0, 0);
                        leverState = LeverState.Neutral;
                        Ship.Stop();
                    }
                    else if (leverState == LeverState.Neutral) //set to Forward if in Neutral
                    {
                        LeverPivot.transform.eulerAngles = new Vector3(45, 0, 0);
                        leverState = LeverState.Forward;
                        Ship.Forward();
                    }
                }
            }
        }

        //Reverse
        if(Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == LeverCollider.name)
                {
                    //set the state of the lever one step backward, unless it is already set to Reverse

                    if (leverState == LeverState.Forward) //set to Neutral if in Forward
                    {
                        LeverPivot.transform.eulerAngles = new Vector3(0, 0, 0);
                        leverState = LeverState.Neutral;
                        Ship.Stop();
                    }
                    else if (leverState == LeverState.Neutral) //set to Reverse if in Neutral
                    {
                        LeverPivot.transform.eulerAngles = new Vector3(-45, 0, 0);
                        leverState = LeverState.Reverse;
                        Ship.Reverse();
                    }
                }
            }
        }

        //Left
        if(Input.GetKey(KeyCode.A))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == SteeringWheel.name)
                {
                    //rotate steering wheel left
                    SteeringWheel.transform.Rotate(0, 0, rotationSpeed);
                    //if doing this turns the z value negative, reset it back to 179
                    if(SteeringWheel.transform.rotation.eulerAngles.z < 0)
                    {
                        SteeringWheel.transform.rotation = Quaternion.Euler(0, 0, 179);
                    }
                    Debug.Log(SteeringWheel.transform.rotation.eulerAngles.ToString());
                }
            }
        }

        //Right
        if(Input.GetKey(KeyCode.D))
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.name == SteeringWheel.name)
                {
                    //rotate steering wheel right
                    SteeringWheel.transform.Rotate(0, 0, -rotationSpeed);
                    //if doing this turns the z value positive, reset it back to -179
                    if (SteeringWheel.transform.rotation.eulerAngles.z > 0)
                    {
                        SteeringWheel.transform.rotation = Quaternion.Euler(0, 0, -179);
                    }
                    Debug.Log(SteeringWheel.transform.rotation.eulerAngles.ToString());
                }
            }
        }
    }
}
