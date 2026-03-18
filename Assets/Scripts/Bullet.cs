using UnityEngine;
using Photon.Pun;


public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo info;
    private Rigidbody rb;
    private PhotonView pv;
   // private GameObject objToMove;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;

        if (other.CompareTag("Player"))
        {
            bool isMineTeam = pv.Owner.CustomProperties.TryGetValue("Team", out object mineTeam);
            bool isOtherTeam = other.GetComponentInParent<PhotonView>().Owner.CustomProperties.TryGetValue("Team", out object otherTeam);
            if(isMineTeam && isOtherTeam)
                if(mineTeam.ToString() != otherTeam.ToString())
                    other.GetComponentInParent<PlayerSetting>().TakeDamage(info.damage);
        }
        PhotonNetwork.Destroy(gameObject);
    }

    public void StartMove(Vector3 dir,GameObject target)
    {
        rb.velocity = dir.normalized*info.speed;
    
    }

    private void OnBecameInvisible()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
