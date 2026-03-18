using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.Events;

public class GameEventsManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCanvas;
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLose;

    private PhotonView pv;

    private void Awake()
    {
        pv = gameObject.GetPhotonView();
    }

    void Start()
    {
        if (!pv.IsMine)
        {

            playerCanvas.SetActive(false);
            return;
        }
    }

    public static void LeaveGame()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(1);
    }
}
