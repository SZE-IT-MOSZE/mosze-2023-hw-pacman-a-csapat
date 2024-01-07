using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float totalTime;
    private bool isGameOver;
    public float remainingTime = 240f;
    public TextMeshProUGUI remainingTimeText;
    public Image timerBar;

    void Start() {
        totalTime = remainingTime;
        ScoreManager.instance.IncreaseTotalTime(totalTime);
    }

    void Update() {
        CalculateAndDisplayTime();
        CheckIfTimeIsUp();
    }

    private void CalculateAndDisplayTime() {

        if (isGameOver) return;

        remainingTime -= Time.deltaTime;

        UpdateDisplayedTimeOnUi();
    }
    
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

    private void CheckIfTimeIsUp() {
        if (!isGameOver && remainingTime <= 0) {
            isGameOver = true;
            remainingTime = 0;

            UpdateDisplayedTimeOnUi();
            Time.timeScale = 0;

            StartCoroutine(gameObject.GetComponent<SceneController>().AnimateUIElements(true));
        }
    }

    void OnDestroy() {
        ScoreManager.instance.IncreaseTotalTimeLeft(remainingTime);
    }
}
