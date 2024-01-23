using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Az id�m�r� vez�rl� oszt�ly.
/// </summary>
public class TimerController : MonoBehaviour {
    /// <summary>
    /// A teljes j�t�kid�.
    /// </summary>
    private float totalTime;

    /// <summary>
    /// Az id�m�r� �llapot�t jelz� v�ltoz�.
    /// </summary>
    private bool isGameOver;

    /// <summary>
    /// A h�tral�v� id�.
    /// </summary>
    public float remainingTime = 240f;

    /// <summary>
    /// A h�tral�v� id�t megjelen�t� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI remainingTimeText;

    /// <summary>
    /// Az id�m�r� cs�k megjelen�t� k�p.
    /// </summary>
    public Image timerBar;

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    void Start() {
        totalTime = remainingTime;
        ScoreManager.instance.IncreaseTotalTime(totalTime);
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        CalculateAndDisplayTime();
        CheckIfTimeIsUp();
    }

    /// <summary>
    /// Az id� kisz�m�t�s�t �s megjelen�t�s�t v�gz� met�dus.
    /// </summary>
    private void CalculateAndDisplayTime() {
        if (isGameOver) return;

        remainingTime -= Time.deltaTime;

        UpdateDisplayedTimeOnUi();
    }

    /// <summary>
    /// Az id� megjelen�t�s�t friss�t� met�dus.
    /// </summary>
    private void UpdateDisplayedTimeOnUi() {
        timerBar.fillAmount = remainingTime / totalTime;

        int hours = Mathf.FloorToInt(remainingTime / 3600f);
        int minutes = Mathf.FloorToInt((remainingTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        string formattedTime;

        if (hours > 0) {
            formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        } else {
            formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        remainingTimeText.text = formattedTime;
    }

    /// <summary>
    /// Ellen�rzi, hogy az id� lej�rt-e.
    /// </summary>
    private void CheckIfTimeIsUp() {
        if (!isGameOver && remainingTime <= 0) {
            isGameOver = true;
            remainingTime = 0;

            UpdateDisplayedTimeOnUi();
            Time.timeScale = 0;

            StartCoroutine(gameObject.GetComponent<SceneController>().AnimateUIElements(true));
        }
    }

    /// <summary>
    /// Az oszt�ly megsemmis�l�sekor megh�v�d� met�dus.
    /// </summary>
    void OnDestroy() {
        if (ScoreManager.instance) {
            ScoreManager.instance.IncreaseTotalTimeLeft(remainingTime);
        }
    }
}
