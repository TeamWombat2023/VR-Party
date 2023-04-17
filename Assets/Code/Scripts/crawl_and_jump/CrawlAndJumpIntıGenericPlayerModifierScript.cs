using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAndJumpIntıGenericPlayerModifierScript : MonoBehaviour
{
    void Start()
    {
        Invoke("GenericPlayerModifier", 10);
    }

    void GenericPlayerModifier(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players) {
            Debug.Log("MODIFIER: "+ player.name);
            player.transform.GetChild(0).GetComponent<CharacterController>().enabled = false;
        }
    }
}
