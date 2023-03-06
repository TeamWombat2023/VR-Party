using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstruction_spawner : MonoBehaviour
{
    public GameObject obstructionPrefab;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(obstructionPrefab, transform.position, Quaternion.identity);
        }
    }
}
