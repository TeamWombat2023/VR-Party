using UnityEngine;
using Photon.Pun;
public class CrawlAndJumpLoseCondition : MonoBehaviour
{
    PhotonView photonView;
    private GameObject kaybedenler_klubu_plane;
    void Start(){
        photonView = GetComponent<PhotonView>();

        kaybedenler_klubu_plane = GameObject.Find("KaybedenlerKlubu");
    }
    void Update()
    {
        if(transform.position.y < -5 && photonView.IsMine){
            transform.position = kaybedenler_klubu_plane.transform.position + new Vector3(0,3,0);
        }
    }
}
