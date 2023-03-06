using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstruction_spawner : MonoBehaviour
{
    public GameObject obstructionPrefab;
    public float random_number = 0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            var clone = Instantiate(obstructionPrefab, transform.position, Quaternion.identity);
            random_number = Random.Range(0,2);
            if (random_number == 0)
            {
                clone.transform.position = clone.transform.position + new Vector3(0, 4, 0);
            }
            Debug.Log(clone.transform.position);
            Debug.Log(random_number);
        }
    }
}
