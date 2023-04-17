using System;
using UnityEngine;

namespace Code.Scripts.Plane_GameCode
{
    public class CheckpointOrder : MonoBehaviour
    {
        
        public GameObject[] Checkpoints;
        private int checkpointIndex;

        private void Start()
        {
            checkpointIndex = 0;

            for (var i = 1; i < Checkpoints.Length; i++)
            {
                Checkpoints[i].SetActive(false); 
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                Debug.Log("Checkpoint obtained");
                EnableNewCheckpoint();
            }
        }
        
        
        public void EnableNewCheckpoint()
        {
            foreach (var checkpoint in Checkpoints)
            {
                checkpoint.gameObject.SetActive(false); 
            }

            Checkpoints[checkpointIndex++].gameObject.SetActive(true);
        }


        private void Update()
        {


        }
    }
}