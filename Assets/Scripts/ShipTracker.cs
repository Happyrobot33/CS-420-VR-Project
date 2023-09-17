using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTracker : MonoBehaviour
{
    public Transform Ship;

    // Update is called once per frame
    void Update()
    {
        transform.position = Ship.position;
    }
}
