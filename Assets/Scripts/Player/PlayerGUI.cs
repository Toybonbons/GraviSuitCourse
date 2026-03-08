using System.Collections;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    
    [Header("GUI Objects")]
    [SerializeField] GameObject HealthBar;
    [SerializeField] GameObject JumpBar, DashBar;

    //BarVals
    private float[] statBarVals = {2,1};
    
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
