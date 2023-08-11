using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverGrabbableReset : MonoBehaviour
{
    public Transform resetPos;
    //[SerializeField] XRBaseInteractable grabbedObject;

    /*private void OnEnable()
    {
        grabbedObject.selectExited.AddListener(LeverReset);
    }

    private void OnDisable()
    {
        grabbedObject.selectExited.RemoveListener(LeverReset);
    }*/


    public void LeverReset(SelectExitEventArgs arg0)
    {
        transform.position = resetPos.position;
        transform.rotation = resetPos.rotation;

        Rigidbody rbhandler = resetPos.GetComponent<Rigidbody>();
        rbhandler.velocity = Vector3.zero;
        rbhandler.angularVelocity = Vector3.zero;
    }

    public void GenericLeverReset()
    {
        transform.position = resetPos.position;
        transform.rotation = resetPos.rotation;
    }

    /*private void Update()
    {
        if (Vector3.Distance(resetPos.position, transform.position) > 0.5f)
        {
            
        }
    }*/
}
