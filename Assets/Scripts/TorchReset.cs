using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorchReset : MonoBehaviour
{
    public Transform ResetPos;
    public GameObject TorchAttachPoint;

    public void Reset()
    {
        transform.position = ResetPos.position;
        transform.rotation = ResetPos.rotation;
        //Debug.Log("Attach point transform reset to: " + TorchAttachPoint.transform.position);
    }
}

#if UNITY_EDITOR
// Add custom GUI Reset button in torch's Inspector pane:
[CustomEditor(typeof(TorchReset))]
public class TorchResetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // First draw the barebones Inspector GUI:
        DrawDefaultInspector();

        TorchReset torchResetScript = (TorchReset)target;
        if (GUILayout.Button("Reset"))
        {
            torchResetScript.Reset();
        }
    }
}
#endif
