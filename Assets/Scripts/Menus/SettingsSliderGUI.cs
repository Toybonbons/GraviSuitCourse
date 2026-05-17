using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSliderGUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] Slider slider;

    [SerializeField] string settingName;
    [SerializeField] int displayMultiplier = 1;

    private SettingsMenu settingsMenuScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsMenuScript = SettingsMenu.Instance;

        slider.onValueChanged.AddListener(updSliderVal);

        loadSettingVal();
    }

    void updSliderVal(float newVal)
    {
        valueText.text = math.round(newVal * displayMultiplier).ToString();

        settingsMenuScript.updSettingVal(settingName, newVal);

        PlayerPrefs.SetFloat(settingName, newVal);
    }

    void loadSettingVal()
    {
        if (!PlayerPrefs.HasKey(settingName))
        {
            valueText.text = slider.value.ToString();
            return;
        }
        
        float settingData = PlayerPrefs.GetFloat(settingName);

        valueText.text = math.round(settingData * displayMultiplier).ToString();
        slider.value = settingData;

        settingsMenuScript.updSettingVal(settingName, settingData);
    }
}
