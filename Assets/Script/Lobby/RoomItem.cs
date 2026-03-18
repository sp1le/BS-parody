using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text roomOnline;
    RoomInfo info;

    public void SetUp(RoomInfo info)
    {
        this.info = info;
        roomName.text = $"Room Name: {info.Name}";
        roomOnline.text = $"{info.PlayerCount}/{info.MaxPlayers}";
    }
    public void OnClick()
    {
        ConnectionToServer.Instance.JoinRoom(this.info);
    }

}
