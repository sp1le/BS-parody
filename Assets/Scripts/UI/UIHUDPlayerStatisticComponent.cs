using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class UIHUDPlayerStatisticComponent : UIPlayerStatistic
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text expText;
    [SerializeField] private TMP_Text teamText;
    private void Start()
    {
        UpdateExpToNextLevel();
        expSlider.maxValue = expToNextLevel;
        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Team", out object team))
            teamText.text = team.ToString();
    }

    private void LateUpdate()
    {
        lvlText.text = playerLevel.ToString();
        expText.text = $"{currentExp}/{expToNextLevel}";
        expSlider.value = currentExp;
    }
}
