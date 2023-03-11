using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaybedenlerKlubu : MonoBehaviour
{
    private GameObject player;
    private GameObject kaybedenler_klubu_plane;


    void Start(){
        player = GameObject.Find("XR Origin");
        kaybedenler_klubu_plane = GameObject.Find("KaybedenlerKlubu");
    }
    void Update()
    {
        if(player.transform.position.y < -5){
            player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            player.transform.position = kaybedenler_klubu_plane.transform.position + new Vector3(0,3,0);
        }
    }
}
