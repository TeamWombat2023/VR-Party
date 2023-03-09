using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMeshes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CombineTheMeshes()
    {

        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;
        
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        
        Debug.Log("It works.");
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        
        Mesh finalMesh = new Mesh();
        
        CombineInstance[] combiners = new CombineInstance[filters.Length];
        for (int i = 0; i < filters.Length; i++)
        {
            combiners[i].subMeshIndex = 0;
            combiners[i].mesh = filters[i].sharedMesh;
            //Dunyada nerdeyse meshtede orada olsun.
            combiners[i].transform = filters[i].transform.localToWorldMatrix;
        }
        finalMesh.CombineMeshes(combiners);
        
        //sharedMesh daha iyi.
        GetComponent<MeshFilter>().sharedMesh = finalMesh;


        transform.rotation = oldRot;
        transform.position = oldPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        
    }
}
