using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsController : MonoBehaviour
{
    private Slider settingSlider;

    private void Awake()
    {
        settingSlider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        settingSlider.onValueChanged.AddListener((float arg)=>GetSettingVal(arg));
    }

    private void GetSettingVal(float val)
    {
        QualitySettings.SetQualityLevel((int)val, true);
    }


}
