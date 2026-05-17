using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("Setting Tabs")]
    [SerializeField] SettingsTabGUI initialTab;
    private SettingsTabGUI activeTab;

    [Header("Misc")]
    [SerializeField] AudioMixer musicMixer;
    
    public static SettingsMenu Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        activeTab = initialTab;
        activeTab.highlightGUI();
    }


    //TABS
    public void changeTab(SettingsTabGUI tabScript, string tabName)
    {
        if (tabScript == activeTab) return;

        activeTab.unhighlightGUI();

        activeTab = tabScript;
    }

    //DATA HANDLING
    public void updSettingVal(string settingName, float value)
    {
        switch (settingName)
        {
            case "Music": musicMixer.SetFloat("Volume", MathF.Log10(value) * 20); break;
            case "Pitch": musicMixer.SetFloat("Pitch", value); break;
        }
    }

}
