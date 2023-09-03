using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampLever : MonoBehaviour
{
    public GameObject LeverPole;
    private float initSidePos;

    // Start is called before the first frame update
    void Start()
    {
        initSidePos = LeverPole.transform.localPosition.z;
        Debug.Log("Initial Z Position found for Lever was" + initSidePos);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Lever's new Z pos is: " + LeverPole.transform.localPosition.z);
        if ((LeverPole.transform.localPosition.z < (initSidePos - 0.1)) || (LeverPole.transform.localPosition.z > (initSidePos + 0.1)))
        {
            // Keep the side-to-side Z-axis the initial z position from the start
            LeverPole.transform.localPosition = new Vector3(LeverPole.transform.localPosition.x, LeverPole.transform.localPosition.y, initSidePos);
        }
    }
}
