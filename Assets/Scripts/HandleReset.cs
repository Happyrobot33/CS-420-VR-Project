using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HandleReset : MonoBehaviour
{
    public Transform ResetPos;

    public void Reset()
    {
        transform.position = ResetPos.position;
        transform.rotation = ResetPos.rotation;
    }
}

#if UNITY_EDITOR
// add a button in inspector to call the Reset() function
[CustomEditor(typeof(HandleReset))]
public class HandleResetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HandleReset myScript = (HandleReset)target;
        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}
#endif
