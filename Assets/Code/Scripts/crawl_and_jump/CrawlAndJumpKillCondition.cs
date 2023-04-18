using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAndJumpKillCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        //other.gameObject.GetParent.transform.GetChild(0).gameObject.transform.position = Vector3.zero;
        //other.gameObject.GetParent.transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity;
        if(other.name == "Body"){
            PlayerManager.LocalXROrigin.transform.position = Vector3.zero;
            PlayerManager.LocalXROrigin.transform.rotation = Quaternion.identity;
        }
    }
}
