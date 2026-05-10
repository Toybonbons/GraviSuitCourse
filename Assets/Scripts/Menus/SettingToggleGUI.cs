using UnityEngine;
using UnityEngine.UI;

public class SettingToggleGUI : MonoBehaviour
{
    [SerializeField] GameObject disabledGui;
    [SerializeField] GameObject enabledGui;
    [SerializeField] Toggle toggle;
    
    [SerializeField] string settingName;


    void Start()
    {
        toggle.onValueChanged.AddListener(changeToggleState);

        loadSettingVal();
    }

    void changeToggleState(bool newState)
    {
        int settingState = 0;

        disabledGui.SetActive(!newState);
        enabledGui.SetActive(newState);

        if (newState) settingState = 1;
        PlayerPrefs.SetInt(settingName, settingState);
    }

    void loadSettingVal()
    {
        if (!PlayerPrefs.HasKey(settingName)) return;
        
        bool settingVal = false;
        int settingData = PlayerPrefs.GetInt(settingName);

        if (settingData == 1) settingVal = true;

        disabledGui.SetActive(!settingVal);
        enabledGui.SetActive(settingVal);

        toggle.isOn = settingVal;
    }
}
