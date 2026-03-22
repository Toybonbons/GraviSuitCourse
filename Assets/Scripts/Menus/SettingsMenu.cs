using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [Header("Setting Tabs")]
    [SerializeField] SettingsTabGUI initialTab;
    private SettingsTabGUI activeTab;
    
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

}
