using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIPlayerStatisticComponent : UIPlayerStatistic
{
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text statusGameText;
    [SerializeField] private Slider curExpSlider;



    private const int LOSE_COEF = 1;
    private const int WIN_COEF = 3;
    private const int EXP_STEP = 50;

    private void UpdateExpUI()
    {
        lvlText.text = $"Level: {playerLevel}";
        expText.text = $"{currentExp}/{expToNextLevel}";
        curExpSlider.value = (float)currentExp / (float)expToNextLevel;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(PLAYER_LEVEL, playerLevel);
        PlayerPrefs.SetInt(PLAYER_EXP, currentExp);
    }

    public void ShowInfo(bool state)
    {
        
        int exp = (playerLevel * EXP_STEP) * (state ? WIN_COEF : LOSE_COEF);
        if (currentExp + exp > expToNextLevel)
        {
            int buffer = (currentExp + exp) - expToNextLevel;
            playerLevel++;
            currentExp = buffer;
            UpdateExpToNextLevel();
        }
        else currentExp += exp;
        statusGameText.text = state ? "You Win" : "You Lose";
        UpdateExpUI();
        SaveData();
 
    }
}
