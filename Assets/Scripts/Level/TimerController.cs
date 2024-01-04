using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float totalTime;
    public float remainingTime = 240f;
    public TextMeshProUGUI remainingTimeText;
    public Image timerBar;

    void Start() {
        totalTime = remainingTime;
        ScoreManager.instance.IncreaseTotalTime(totalTime);
    }

    void Update() {
        CalculateAndDisplayTime();
    }

    void CalculateAndDisplayTime() {
        remainingTime -= Time.deltaTime;

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

    void OnDestroy() {
        ScoreManager.instance.IncreaseTotalTimeLeft(remainingTime);
    }
}
