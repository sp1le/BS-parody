using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.Events;
public class GameNetworkManager : MonoBehaviour
{
    [SerializeField] private GameObject allPlayerUI;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameWin;
    private PhotonView pv;

    private void Awake()
    {
        pv = gameObject.GetPhotonView();
    }
    void Start()
    {
        if(!pv.IsMine)
        {
            allPlayerUI.SetActive(false);
            return;
        }
    }

    public void Gamewin()
    {
        Time.timeScale = 0f;
    }

    public static void OutOfBattle()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
