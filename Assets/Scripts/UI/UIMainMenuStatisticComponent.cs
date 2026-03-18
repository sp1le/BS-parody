using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainMenuStatisticComponent : UIPlayerStatistic
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text expText;
    private void Start()
    {
        UpdateExpToNextLevel();
        expSlider.maxValue = expToNextLevel;
    }

    private void LateUpdate()
    {
        print(playerLevel);
        lvlText.text = playerLevel.ToString();
        expText.text = $"{currentExp}/{expToNextLevel}";
        expSlider.value = currentExp;
    }
}
