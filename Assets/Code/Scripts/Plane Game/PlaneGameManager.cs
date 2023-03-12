using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaneGameManager : MonoBehaviour
{
    public GameObject checkPointsHolder;

    private Checkpoint[] _checkpoints;
    private int _currentCheckpoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _checkpoints= checkPointsHolder.GetComponentsInChildren<Checkpoint>();
        _currentCheckpoint = 0;
        EnableNewCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void EnableNewCheckpoint()
    {
         foreach (var checkpoint in _checkpoints)
         {
             checkpoint.gameObject.SetActive(false); 
         }

         _checkpoints[_currentCheckpoint++].gameObject.SetActive(true);
    }

}
