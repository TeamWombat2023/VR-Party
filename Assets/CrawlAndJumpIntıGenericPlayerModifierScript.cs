using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAndJumpIntıGenericPlayerModifierScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject generic_player;
    void Start()
    {
        Invoke("GenericPlayerModifier", 10);
    }

    void GenericPlayerModifier(){
        //players = FindObjectsOfType<typeof(generic_player)>();

        //foreach (generic_player player in players) {
        //    Debug.Log(player.name);
        //}
    }
}
