using System;
using Photon.Pun;
using UnityEngine;

namespace Code.Scripts.Manager
{
    
    
    public class NetworkManagerLabrynth : NetworkManager
    {
        
        [SerializeField] private GameObject genericVRPlayerPrefab;
        [SerializeField] private Vector3 spawnPosition;
        
        private void Start()
        {
            Debug.Log("Joined the game");
            GameObject _player = PhotonNetwork.Instantiate(genericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }
}