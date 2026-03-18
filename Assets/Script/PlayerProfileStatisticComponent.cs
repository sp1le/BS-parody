using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
public class PlayerProfileStatisticComponent : PlayerStatisticComponent
{
    [SerializeField] private TMP_Text lvlText;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text curExpText;
    [SerializeField] private Slider curExpSlider;

    void Start()
    {
        UpdateEXPToNextLevel();
        lvlText.text = playerLevel.ToString();
        usernameText.text = PhotonNetwork.LocalPlayer.NickName;
        curExpText.text = $"{currentEXP}/{EXPtoNextLevel}";
        curExpSlider.value = (float)currentEXP / (float)EXPtoNextLevel;
    }

}
