using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RigidbodyKill : MonoBehaviour
{
    Collider col;

    //water enter prefab
    public GameObject waterEnterPrefab;

    void Start()
    {
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision other)
    {
        //check if the collision is a rigidbody
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            //kill the rigidbody
            Destroy(other.gameObject);

            //instantiate the water enter prefab at the collision point
            Instantiate(waterEnterPrefab, other.contacts[0].point, Quaternion.identity);
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
