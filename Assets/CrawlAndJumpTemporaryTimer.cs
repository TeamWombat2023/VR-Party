using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAndJumpTemporaryTimer : MonoBehaviour
{
    public CrawlAndJumpScore scoring_script;
    void Start()
    {
        scoring_script.begin_timer();
        Debug.Log("BEGIN:" + scoring_script.start);
    }
}
