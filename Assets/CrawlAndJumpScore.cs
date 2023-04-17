using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAndJumpScore : MonoBehaviour
{
    public System.DateTime start;
    public System.DateTime end;
    public System.TimeSpan interval;
    public void begin_timer(){
        start = System.DateTime.Now;
    }
    public void end_timer(){
        end = System.DateTime.Now;
        interval = end - start;
        Debug.Log("INTERVAL:" + interval.TotalSeconds);
    }
}
