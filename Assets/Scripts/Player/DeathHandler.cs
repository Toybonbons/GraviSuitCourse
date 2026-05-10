using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] AudioSource music;


    public void startDeath()
    {
        Debug.Log("Have died!");
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine(musicFade());
        StartCoroutine(exitDelay());
    }


    IEnumerator musicFade()
    {
        float time = 3;
        float currentVol = music.volume, currentPitch = music.pitch;

        while (time >= 0)
        {
            time -= Time.unscaledDeltaTime;
            Debug.Log(time);

            float progressVal = time / 3;

            music.volume = currentVol * progressVal;
            music.pitch = currentPitch * progressVal;

            yield return new WaitForEndOfFrame();
        }

        music.volume = 0;
    }

    IEnumerator exitDelay()
    {
        yield return new WaitForSecondsRealtime(8);

        SceneManager.LoadScene(0);
    }
}
