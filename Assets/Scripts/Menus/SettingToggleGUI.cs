using UnityEngine;
using UnityEngine.UI;

public class SettingToggleGUI : MonoBehaviour
{
    [SerializeField] GameObject disabledGui;
    [SerializeField] GameObject enabledGui;
    [SerializeField] Toggle toggle;


    void Start()
    {
        toggle.onValueChanged.AddListener(changeToggleState);
    }

    void changeToggleState(bool newState)
    {
        disabledGui.SetActive(!newState);
        enabledGui.SetActive(newState);
    }
}
