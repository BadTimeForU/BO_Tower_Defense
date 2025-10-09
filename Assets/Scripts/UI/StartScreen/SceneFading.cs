using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;                 // Het zwarte overlay Image
    public float fadeDuration = 1f;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 3f;

    [Header("Countdown toggles")]
    public bool useCountdownOnFadeIn = true;   // zet deze aan/uit in Inspector
    public bool useCountdownOnFadeOut = true;  // zet deze aan/uit in Inspector

    void Start()
    {
        StartCoroutine(FadeIn());  // fade-in bij start
    }

    public void PlayGame(string Game)
    {
        StartCoroutine(FadeOut(Game));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!"); // werkt alleen in build
    }

    IEnumerator FadeIn()
    {
        // Countdown alleen als de bool aan staat
        if (useCountdownOnFadeIn)
        {
            float timeLeft = countdownTime;
            while (timeLeft > 0)
            {
                countdownText.text = Mathf.Ceil(timeLeft).ToString();
                yield return new WaitForSeconds(1f);
                timeLeft--;
            }
            countdownText.text = "";
        }

        // Fade naar transparant
        float t = fadeDuration;
        Color c = fadeImage.color;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut(string Game)
    {
        // Countdown alleen als de bool aan staat
        if (useCountdownOnFadeOut)
        {
            float timeLeft = countdownTime;
            while (timeLeft > 0)
            {
                countdownText.text = Mathf.Ceil(timeLeft).ToString();
                yield return new WaitForSeconds(1f);
                timeLeft--;
            }
            countdownText.text = "";
        }

        // Fade naar zwart
        float t = 0f;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(Game);
    }
}
