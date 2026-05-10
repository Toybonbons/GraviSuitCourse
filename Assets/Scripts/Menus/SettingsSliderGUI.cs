using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSliderGUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] Slider slider;

    [SerializeField] string settingName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.onValueChanged.AddListener(updSliderVal);

        loadSettingVal();
    }

    void updSliderVal(float newVal)
    {
        valueText.text = newVal.ToString();

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

        valueText.text = settingData.ToString();
        slider.value = settingData;
    }
}
