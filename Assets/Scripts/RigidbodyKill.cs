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
            //destroy the rigidbody once all particle systems are finished
            StartCoroutine(WaitForParticleSystems(other.gameObject));

            //instantiate the water enter prefab at the collision point
            Instantiate(waterEnterPrefab, other.contacts[0].point, Quaternion.identity);
        }
    }

    //coroutine to wait for the particle systems on anything that enters the water
    IEnumerator WaitForParticleSystems(GameObject other)
    {
        //get all particle systems on the object
        ParticleSystem[] particleSystems = other.GetComponentsInChildren<ParticleSystem>();

        //move under the water collider
        other.transform.position = new Vector3(other.transform.position.x, -100, other.transform.position.z);

        //loop through all particle systems
        foreach (ParticleSystem ps in particleSystems)
        {
            //disable emission
            ps.Stop();
            //wait for the particle system to finish
            while (ps.IsAlive())
            {
                yield return null;
            }
        }

        //destroy the object
        Destroy(other);
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
