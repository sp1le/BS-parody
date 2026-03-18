
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;



public class PlayerSetting : MonoBehaviourPunCallbacks
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthBar;
    private int health;
    private PhotonView pv;

    private const byte GAME_IS_WIN = 0;
    private const byte SCORE_UPDATED = 1;
    private GameEventsManager gameManager;
    
    int counter = 0;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        gameManager = GetComponentInParent<GameEventsManager>();

    }
    void Start()
    {
        health = maxHealth;
        healthBar.maxValue = health;
        healthBar.value = health;
        

        if (pv.Owner != null)
        {
            
            if (pv.Owner.CustomProperties.TryGetValue("Team", out object team))
            {
                GameObject background = healthBar.transform.Find("Background").gameObject;
                GameObject fillArea = healthBar.transform.Find("Fill Area/Fill").gameObject;
                gameObject.tag = team.ToString();
                if (ColorUtility.TryParseHtmlString(team.ToString(), out Color color))
                {
                    Color.RGBToHSV(color, out float h, out float s, out float v);
                    Color backColor = Color.HSVToRGB(h, s, v * 0.7f);
                    fillArea.GetComponent<Image>().color = color;
                    background.GetComponent<Image>().color = backColor;
                }
            }
        }
    }

   
    public override void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnBattleFinished;
        PhotonNetwork.NetworkingClient.EventReceived += OnScoreUpdated;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnBattleFinished;
        PhotonNetwork.NetworkingClient.EventReceived -= OnScoreUpdated;
    }

    private void OnBattleFinished(EventData data)
    {
        if(data.Code == GAME_IS_WIN)
        {
            gameManager.OnGameWin.Invoke();
            Invoke(nameof(LeaveGame), 5f);
        }
      
    }

    private void OnScoreUpdated(EventData data)
    {
        if (data.Code == SCORE_UPDATED)
        {
            object[] recieveData = (object[]) data.CustomData;
            GetComponentInParent<GameManager>().UpdateScoreUI(recieveData[0].ToString());
        }
    }


    private void SendEvent()
    {
        gameObject.tag = "Untagged";
        bool isTeam = pv.Owner.CustomProperties.TryGetValue("Team", out object team);
        if (isTeam)
        {         
            int countEnemies = GameObject.FindGameObjectsWithTag(team.ToString()).Length;
            if (countEnemies == 0)
            {
                PhotonNetwork.RaiseEvent(GAME_IS_WIN, null, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            }
                object[] data = new object[]{team.ToString()};
                PhotonNetwork.RaiseEvent(SCORE_UPDATED, data, RaiseEventOptions.Default, SendOptions.SendUnreliable); 
        }
    }

    public void TakeDamage(int val)
    {
        pv.RPC(nameof(UpdateDamage), RpcTarget.All, val);
    }

    [PunRPC]
    public void UpdateDamage(int val)
    {
        health -= val; 
        if(health <= 0)
        {
            healthBar.value = health;
            if (!pv.IsMine) return;
            SendEvent();
            gameManager.OnGameLose.Invoke();
            PhotonNetwork.Destroy(transform.Find("Render").gameObject);
      
            Invoke(nameof(LeaveGame), 2f);
        }
        healthBar.value = health;
    }


    public void LeaveGame()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(1);
    }
}
