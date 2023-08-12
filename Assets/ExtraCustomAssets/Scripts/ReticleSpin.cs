using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleSpin : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        //Rotate only about the Y axis since should be top-down rot.:
        transform.Rotate(0f, 0.5f, 0f, Space.Self);
    }
}
