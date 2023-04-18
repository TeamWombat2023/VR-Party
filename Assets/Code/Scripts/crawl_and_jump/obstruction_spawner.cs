using UnityEngine;
using Photon.Pun;
public class obstruction_spawner : MonoBehaviourPunCallbacks
{
    public GameObject obstructionPrefab;
    private float top_or_bottom = 0f;
    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space)){
    //        var clone = Instantiate(obstructionPrefab, transform.position, Quaternion.identity);
    //        top_or_bottom = Random.Range(0,2);
    //        if(Random.Range(0,2) == 0){
    //            clone.GetComponent<ObstructionMovement>().isRotating = true;
    //        }
    //        if (top_or_bottom == 0)
    //        {
    //            clone.transform.position = clone.transform.position + new Vector3(0, 3, 0);
    //        }
    //    }
    //}

    void Start(){
        InvokeRepeating("obstruction_spawn", 5f, 3f);
    }
    void obstruction_spawn(){
        if(PhotonNetwork.IsMasterClient){
        var clone = Instantiate(obstructionPrefab, transform.position, Quaternion.identity);
        top_or_bottom = Random.Range(0,2);
        if(Random.Range(0,2) == 0){
            clone.GetComponent<ObstructionMovement>().isRotating = true;
        }
        if (top_or_bottom == 0)
        {
            clone.transform.position = clone.transform.position + new Vector3(0, 1.75f, 0);
        }
        }
    }
}
