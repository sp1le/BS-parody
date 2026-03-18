using UnityEngine;

public class UIPlayerStatistic : MonoBehaviour
{

    protected int playerLevel;
    protected int currentExp;
    protected int expToNextLevel;
    protected const int BASE_EXP = 500;

    protected string PLAYER_LEVEL = "PlayerLvl";
    protected string PLAYER_EXP = "PlayerExprs";

    private void Awake()
    {
        currentExp = PlayerPrefs.GetInt(PLAYER_EXP);
        playerLevel = PlayerPrefs.GetInt(PLAYER_LEVEL) == 0 ? 1 : PlayerPrefs.GetInt(PLAYER_LEVEL);
    }

    protected void UpdateExpToNextLevel()
    {
        expToNextLevel = BASE_EXP + (BASE_EXP * playerLevel)/2;
    }
}

