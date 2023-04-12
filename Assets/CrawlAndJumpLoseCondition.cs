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
        Transform body_transform = transform.GetChild(0);
        //print(body_transform.position);
        if(body_transform.position.y < -5 && photonView.IsMine){
            body_transform.position = kaybedenler_klubu_plane.transform.position + new Vector3(0,3,0);
        }
    }
}
