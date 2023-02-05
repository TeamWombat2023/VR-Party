using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class PhotonMovementCheck : MonoBehaviour
{
    public XROrigin activePlayer;

    public PhotonView activePhotonView;
    // Start is called before the first frame update
    void Start()
    {
        activePhotonView = activePlayer.GetComponent<PhotonView>();
        
        
        //Yapılacak işler, eğer bu bizim XROriginimizse yapılmalı.


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
