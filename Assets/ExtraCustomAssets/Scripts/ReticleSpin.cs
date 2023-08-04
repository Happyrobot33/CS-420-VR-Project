using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleSpin : MonoBehaviour
{
    //public GameObject thisReticle;

    private float rotX;
    private float rotY;
    private float rotZ;
    //public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch initial rotation values of this game object:
        //(NOTE: using thisGameObject is redundant)
        //rotX = thisReticle.transform.rotation.eulerAngles.x;
        //rotY = thisReticle.transform.rotation.eulerAngles.y;
        //rotZ = thisReticle.transform.rotation.eulerAngles.z;

        rotX = transform.rotation.eulerAngles.x;
        rotY = transform.rotation.eulerAngles.y;
        rotZ = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate only about the Y axis since should be top-down rot.:
        transform.Rotate(0f, 0.5f, 0f, Space.Self);
        rotY = transform.rotation.eulerAngles.y;
        //transform.Rotate(new Vector3(0, 30f, 0) * Time.deltaTime * speed);
        //Debug.Log("I am running: new rotation is: " + thisReticle.transform.rotation.eulerAngles.y);
        Debug.Log("I am running: new rotation is: " + rotY);

        //CODE UPDATE: Didn't really need initial rotations but leave
        //for future changes if needed,
    }
}
