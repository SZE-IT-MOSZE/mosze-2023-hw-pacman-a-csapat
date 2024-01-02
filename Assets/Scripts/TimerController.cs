using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float totalTime;
    public float remainingTime;
    private TextMeshProUGUI timerClock;
    private Image timerBar;

    void Start()
    {
        timerClock = GameObject.Find("TimerClock").GetComponent<TextMeshProUGUI>();
        timerBar = GameObject.Find("TimerBar").GetComponent<Image>();
        totalTime = remainingTime = 240f;
    }

    void Update() {
        CalculateAndDisplayTime();
    }

    public void SaveElapsedTime() {
        // Idõ elmentése
        //PlayerPrefs.SetFloat("ElapsedTime", 240f - remainingTime);
        //PlayerPrefs.Save();
    }

    private void CalculateAndDisplayTime() {
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

        timerClock.text = formattedTime;
    }

    public void decreaseTime(float time)
    {
        remainingTime -= time;
    }
}
