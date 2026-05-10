using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //GUI
    [Header("GUI Elements")]
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject loadingScreen, settingsScreen, achievementsScreen;

    [SerializeField] Button storyModeButton, endlessModeButton;
    [SerializeField] Button settingsButton, achievementsButton;

    //Audio
    [Header("Audio")]
    [SerializeField] AudioSource music;

    //Values
    [Header("Values")]
    public string status;
    private GameObject activeScreen;

    //Instancing
    public static MainMenu Instance;

    void Awake()
    {
        MainMenu.Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;

        status = "MainScreen";
        activeScreen = mainScreen;

        StartCoroutine(musicStart());
        setupButtons();
    }

    //Setup
    void setupButtons()
    {
        void settingsMenu()
        {
            changeScreens(settingsScreen);
        }

        
        endlessModeButton.onClick.AddListener(loadEndlessMode);

        settingsButton.onClick.AddListener(settingsMenu);
    }

    IEnumerator musicStart()
    {
        float time = 0;

        while (time <= 2)
        {
            time += Time.deltaTime;
            Debug.Log(time);

            float progressVal = time / 2;

            music.volume = progressVal;
            music.pitch = progressVal * .9f;

            yield return new WaitForEndOfFrame();
        }
    }

    //Scene Transition
    void loadEndlessMode()
    {
        showLoadingScreen();
        SceneManager.LoadScene(1);
    }

    //Menu Navi
    public void returnToMain()
    {
        status = "MainScreen";

        activeScreen.SetActive(false);
        mainScreen.SetActive(true);

        activeScreen = mainScreen;
    }

    public void changeScreens(GameObject newScreen)
    {
        status = newScreen.name;

        activeScreen.SetActive(false);
        newScreen.SetActive(true);

        activeScreen = newScreen;
    }

    public void showLoadingScreen()
    {
        status = "Loading";

        activeScreen.SetActive(false);
        loadingScreen.SetActive(true);

        activeScreen = loadingScreen;
    }
}
