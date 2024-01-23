using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Az idõmérõ vezérlõ osztály.
/// </summary>
public class TimerController : MonoBehaviour {
    /// <summary>
    /// A teljes játékidõ.
    /// </summary>
    private float totalTime;

    /// <summary>
    /// Az idõmérõ állapotát jelzõ változó.
    /// </summary>
    private bool isGameOver;

    /// <summary>
    /// A hátralévõ idõ.
    /// </summary>
    public float remainingTime = 240f;

    /// <summary>
    /// A hátralévõ idõt megjelenítõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI remainingTimeText;

    /// <summary>
    /// Az idõmérõ csík megjelenítõ kép.
    /// </summary>
    public Image timerBar;

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    void Start() {
        totalTime = remainingTime;
        ScoreManager.instance.IncreaseTotalTime(totalTime);
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        CalculateAndDisplayTime();
        CheckIfTimeIsUp();
    }

    /// <summary>
    /// Az idõ kiszámítását és megjelenítését végzõ metódus.
    /// </summary>
    private void CalculateAndDisplayTime() {
        if (isGameOver) return;

        remainingTime -= Time.deltaTime;

        UpdateDisplayedTimeOnUi();
    }

    /// <summary>
    /// Az idõ megjelenítését frissítõ metódus.
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
    /// Ellenõrzi, hogy az idõ lejárt-e.
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
    /// Az osztály megsemmisülésekor meghívódó metódus.
    /// </summary>
    void OnDestroy() {
        if (ScoreManager.instance) {
            ScoreManager.instance.IncreaseTotalTimeLeft(remainingTime);
        }
    }
}
