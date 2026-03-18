using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMenuStatisticComponent : PlayerStatisticComponent
{
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] protected TMP_Text statusGameText;
    [SerializeField] protected Slider curExpSlider;

    private const int LOSE_COEF = 1;
    private const int WIN_COEF = 3;
    private const int EXP_STEP = 50;
    private void UpdateText()
    {
        lvlText.text = $"Level: {playerLevel}";
        expText.text = $"{playerLevel}/{EXPtoNextLevel}";
        curExpSlider.value = (float)currentEXP / (float)EXPtoNextLevel;
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("PlayerExp", currentEXP);
    }

    public void ShowInfo(bool state)
    {
        int val = playerLevel * EXP_STEP * (state ? WIN_COEF : LOSE_COEF);
        if (currentEXP + val > EXPtoNextLevel)
        {
            currentEXP = (currentEXP + val) - EXPtoNextLevel;
            playerLevel++;
            UpdateEXPToNextLevel();
        }
        else currentEXP += val;
        statusGameText.text = state ? "You Win" : "You Lose";
        UpdateText();
        SaveData();
    }
}
