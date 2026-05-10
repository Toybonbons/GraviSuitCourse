using System.Collections;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [Header("Death Screen")]
    [SerializeField] GameObject DeathScreen;

    [Header("GUI Objects")]
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject JumpBar, DashBar;

    //BarVals
    private float[] statBarVals = {2,1,100};
    
    //Instancing
    public static PlayerGUI instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        setupStatBars();
    }

    //Initial Setup
    void setupStatBars()
    {
        JumpBar.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        DashBar.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }

    //Death Screen
    public void showDeathScreen()
    {
        DeathScreen.SetActive(true);
    }

    //Upd Stat Bars
    public void updJumps(float jumpCount)
    {
        float barProgress = jumpCount / 2;
        statBarVals[0] = jumpCount;

        StartCoroutine(lerpStatBar(JumpBar.GetComponent<RectTransform>(), barProgress, 0));
    }

    public void updDashes(float dashCount)
    {
        float barProgress = dashCount;
        statBarVals[1] = dashCount;
        
        StartCoroutine(lerpStatBar(DashBar.GetComponent<RectTransform>(), barProgress, 1));
    }

    public void updHealth(float healthVal)
    {
        float barProgress = healthVal / 100;
        statBarVals[2] = healthVal;

        StartCoroutine(lerpStatBar(HealthBar.GetComponent<RectTransform>(), barProgress, 2));
    }


    IEnumerator lerpStatBar(RectTransform statBar, float targetVal, int statIndex)
    {
        float time = 0f;
        float savedVal = statBarVals[statIndex];
        
        while (time <= 0.5)
        {
            if (savedVal != statBarVals[statIndex]) break;

            time += Time.deltaTime;

            statBar.localScale = Vector3.Lerp(statBar.localScale, new Vector3(targetVal,1,1), time * 2);

            yield return new WaitForEndOfFrame();
        }
    }

}
