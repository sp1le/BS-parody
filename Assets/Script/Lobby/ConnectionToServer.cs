using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance;
    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private Transform transformRoomList;
    [SerializeField] private Transform transformPlayerList;
    [SerializeField] private GameObject roomItemPrefab; 
    [SerializeField] private GameObject playerListItem;
    [Space(10)]
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private TMP_Text waitBattleText;
   
    private string[] teams = {"green","red"};
   
    private bool isPvP;
    private void Awake() 
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
       PhotonNetwork.JoinLobby();
       
    }
    public override void OnJoinedLobby()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");
        Debug.Log("Connected to Lobby!");
    }
    public void CreateNewRoom()
    {
        if(string.IsNullOrEmpty(inputRoomName.text))
        {
            return;
        }
        RoomOptions currentRoom = new RoomOptions();
        currentRoom.IsOpen = true;
        currentRoom.MaxPlayers = 10;
        PhotonNetwork.CreateRoom(inputRoomName.text, currentRoom);
    }

    public override void OnJoinedRoom()
    {
        print("Player Joined");
        PhotonNetwork.AutomaticallySyncScene = true;
        if (isPvP)
        {
            if (PhotonNetwork.IsMasterClient) return;
            waitBattleText.text = "The battle begins!";
        }
        else
        {
            Player[] players = PhotonNetwork.PlayerList;
            WindowsManager.Layout.OpenLayout("GameRoom");
            if (PhotonNetwork.IsMasterClient)
            {

                startGameButton.SetActive(true);
            }
            else
            {
                startGameButton.SetActive(false);
                waitBattleText.text = "The battle begins!";
            }

            roomName.text = PhotonNetwork.CurrentRoom.Name;

            foreach (Transform trns in transformPlayerList)
            {
                Destroy(trns.gameObject);
            }

            if (PhotonNetwork.IsMasterClient) SetTeam(players[0], 0);
            foreach(Player player in players)
            {
                StartCoroutine(GetTeam(player));
            }
        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        
        if(PhotonNetwork.IsMasterClient) startGameButton.SetActive(true);           
        else startGameButton.SetActive(false);
    }
    public void ConnectToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        WindowsManager.Layout.OpenLayout("MainMenu");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trns in transformRoomList)
        {
           Destroy(trns.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomItemPrefab, transformRoomList).GetComponent<RoomItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("Player entered");
        if (isPvP)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            Room curRoom = PhotonNetwork.CurrentRoom;
            curRoom.IsOpen = false;
            waitBattleText.text = "The battle begins";
            Invoke(nameof(LoadingGameMap), 3f);
        }
        else {
            Player[] players = PhotonNetwork.PlayerList;
            foreach (Transform trns in transformPlayerList)
            {
                Destroy(trns.gameObject);
            }
            for(int i=0;i<players.Length;i++)
            {
                SetTeam(players[i], i % 2 == 0 ? 0 : 1);
                StartCoroutine(GetTeam(players[i]));
            }
        }
    }
    public void StartGameLevel(int levelIndex)
    {
        PhotonNetwork.LoadLevel(levelIndex);
    
    }

    public void ToBattleButton()
    {
        WindowsManager.Layout.OpenLayout("AutomaticBattle");
        isPvP = true;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if(returnCode == (short)ErrorCode.NoRandomMatchFound)
        {
            waitBattleText.text = "Not matches found";
            
            CreateNewRoom(RoomNameGenerator());
        }
    }

    private void CreateNewRoom(string name)
    {
        RoomOptions currentRoom = new RoomOptions();
        currentRoom.IsOpen = false;
        currentRoom.MaxPlayers = 20;
        PhotonNetwork.CreateRoom(name,currentRoom);
    }
    private string RoomNameGenerator()
    {
        short codeLength = 12;
        string roomCode = null;
        for(short i =0; i < codeLength; i++)
        {
            char symbol = (char)Random.Range(65, 91);
            roomCode += symbol;
        }
        return roomCode;
    }
    public override void OnCreatedRoom()
    {
        waitBattleText.text = "waiting for the second player";
    }

    public void LoadingGameMap()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public void StopFindBattleBut()
    {
        PhotonNetwork.LeaveRoom();
        isPvP = false;
    }

   private void SetTeam(Player player, int index)
   {
        Hashtable playerTeam = new Hashtable
        {
            {
               "Team",teams[index]
            }
        };

        player.SetCustomProperties(playerTeam);
   }
   private IEnumerator GetTeam(Player player)
   {
        yield return new WaitForSeconds(1f);
        object team;
        string playerTeam = "";
        if (player.CustomProperties.TryGetValue("Team", out team))
        {
             playerTeam = team.ToString();
        }
        Instantiate(playerListItem, transformPlayerList).GetComponent<PlayerListItem>().SetUp(player, playerTeam);
    }
}
