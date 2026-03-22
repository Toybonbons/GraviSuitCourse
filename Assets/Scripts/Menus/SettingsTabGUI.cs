using UnityEngine;
using UnityEngine.UI;

public class SettingsTabGUI : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Image GUIImage;

    SettingsMenu settingsMenu;

    void Awake()
    {
        unhighlightGUI();
    }

    void Start()
    {
        settingsMenu = SettingsMenu.Instance;

        button.onClick.AddListener(clicked);
    }

    void clicked()
    {
        highlightGUI();
        settingsMenu.changeTab(this, gameObject.name);
    }


    //GUI Anims
    public void highlightGUI()
    {
        GUIImage.color = new Color(0.1f, 0.17f, 0.4f);
    }

    public void unhighlightGUI()
    {
        GUIImage.color = new Color(0.05f, 0.0875f, 0.2f);
    }
}
