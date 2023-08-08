using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AimableObject : MonoBehaviour
{
    public GameObject grabEmpty;

    [Range(0, 180)]
    public float horizontalAngleLimit = 50f;

    [Range(0, 180)]
    public float verticalAngleLimit = 50f;
    GameObject parent;
    public GameObject horizontalHinge;
    public GameObject verticalHinge;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtGrabbedPosition();

        //get the difference between Y rotation of this object and the parent
        float yAngle = transform.localEulerAngles.y;
        //if the angle is greater than 180, subtract 360 to get the negative angle
        if (yAngle > 180)
        {
            yAngle -= 360;
        }

        //get the difference between X rotation of this object and the parent
        float xAngle = transform.localEulerAngles.x;
        //if the angle is greater than 180, subtract 360 to get the negative angle
        if (xAngle > 180)
        {
            xAngle -= 360;
        }

        //clamp the angles to the limits
        yAngle = Mathf.Clamp(yAngle, -horizontalAngleLimit, horizontalAngleLimit);
        xAngle = Mathf.Clamp(xAngle, -verticalAngleLimit, verticalAngleLimit);

        //set the local rotation of this object to the clamped angles
        transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);

        //set the local rotation of the horizontal hinge to the y angle
        horizontalHinge.transform.localEulerAngles = new Vector3(0, yAngle, 0);
        //set the local rotation of the vertical hinge to the x angle
        verticalHinge.transform.localEulerAngles = new Vector3(xAngle, 0, 0);
    }

    /// <summary> Make this object's -z axis point at the grabEmpty </summary>
    private void LookAtGrabbedPosition()
    {
        //make this object's z axis point at the grabEmpty
        transform.LookAt(grabEmpty.transform);
        //flip it to make it -z
        transform.Rotate(0, 180, 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //draw a line from this object to the grabEmpty
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, grabEmpty.transform.position);

        //get parent
        parent = transform.parent.gameObject;

        //draw a flat cone to show the horizontal angle limit
        DrawAngleGizmo(parent.transform, parent.transform.up, horizontalAngleLimit);

        //draw a flat cone to show the vertical angle limit
        DrawAngleGizmo(parent.transform, parent.transform.right, verticalAngleLimit);

        #region Compound Angle Limit Gizmo
        //draw a line to represent the current angle
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);

        //get the angle between the parent's forward and this object's forward
        float angle = Vector3.Angle(parent.transform.forward, transform.forward);

        //determine normal direction
        Vector3 cross = Vector3.Cross(parent.transform.forward, transform.forward);

        //draw a arc to represent the current angle
        Handles.color = Color.green;
        Handles.DrawWireArc(transform.position, cross, parent.transform.forward, angle, 1f);
        #endregion
    }

    private void DrawAngleGizmo(Transform t, Vector3 axis, float angle)
    {
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, axis, t.forward, angle, 1f);
        Handles.DrawWireArc(transform.position, axis, t.forward, -angle, 1f);

        //draw lines to the edge of the angle limit
        Vector3 edge1 = Quaternion.AngleAxis(angle, axis) * t.forward;
        Vector3 edge2 = Quaternion.AngleAxis(-angle, axis) * t.forward;
        Handles.DrawLine(transform.position, transform.position + edge1);
        Handles.DrawLine(transform.position, transform.position + edge2);
    }
#endif
}
