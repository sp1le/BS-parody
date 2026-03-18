using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text redScoreText;
    [SerializeField] private TMP_Text greenScoreText;
    [SerializeField] private int startMinutes, startSeconds;
    public static int greenTeamScore, redTeamScore;
    private int curTimeSeconds;
    private PhotonView pv;
    private GameEventsManager gm;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        gm = GetComponent<GameEventsManager>();
        curTimeSeconds = startMinutes * 60 + startSeconds;
        InvokeRepeating(nameof(Timer), 0f, 1f);
    }

    private void CheckWinner()
    {
        Time.timeScale = 0f;
        bool isTeamGot = pv.Owner.CustomProperties.TryGetValue("Team", out object team);
        if (isTeamGot && (redTeamScore > greenTeamScore))
        {
            if (team.ToString() == "green") gm.OnGameLose.Invoke();
            else gm.OnGameWin.Invoke();
        }
        else if (isTeamGot && (greenTeamScore > redTeamScore))
        {
            if (team.ToString() == "red") gm.OnGameLose.Invoke();
            else gm.OnGameWin.Invoke();
        }
        Time.timeScale = 1f;
    }

    private void Timer()
    {
        int minutes = curTimeSeconds / 60;
        int seconds = curTimeSeconds % 60;
        timeText.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        curTimeSeconds--;

        if(minutes <= 0 && seconds <= 0)
        {
            CheckWinner();
        }
    }

    public void UpdateScoreUI(string team)
    {
        if(team == "green")
        {
            redTeamScore++;
            redScoreText.text = redTeamScore.ToString();
        }
        else if(team == "red")
        {
            greenTeamScore++;
            greenScoreText.text = greenTeamScore.ToString();
        }

    }

}
