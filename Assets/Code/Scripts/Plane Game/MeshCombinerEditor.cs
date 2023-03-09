using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CombineMeshes))]
public class MeshCombinerEditor : Editor
{
    private void OnSceneGUI()
    {
        CombineMeshes cm = target as CombineMeshes;
        if (Handles.Button(cm.transform.position+ Vector3.up*5,Quaternion.LookRotation(Vector3.up),1,1, Handles.CylinderHandleCap ))
        {
           cm.CombineTheMeshes(); 
        } 
    }
}
