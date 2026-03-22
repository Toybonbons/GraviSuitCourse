using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    MainMenu menuScript;
    Button button;

    void Start()
    {
        menuScript = MainMenu.Instance;
        button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(buttonClick);
    }

    void buttonClick()
    {
        menuScript.returnToMain();
    }
}
