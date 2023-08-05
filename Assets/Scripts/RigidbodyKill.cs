using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RigidbodyKill : MonoBehaviour
{
    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        //check if the collision is a rigidbody
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            //kill the rigidbody
            Destroy(collision.gameObject);
        }
    }
}

//custom inspector
#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(RigidbodyKill))]
public class RigidbodyKillEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //description
        UnityEditor.EditorGUILayout.HelpBox(
            "This script will kill any rigidbodys that hit the collider attached to this object.",
            UnityEditor.MessageType.Info
        );
    }
}
#endif
