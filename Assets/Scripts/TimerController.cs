using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private float totalTime;
    public float remainingTime;
    private float timeBarSizeUnitX;
    public TextMeshProUGUI remainingTimeText;
    public Image timerBar;

    void Start()
    {
        totalTime = remainingTime = 240f;

        timeBarSizeUnitX = timerBar.fillAmount / remainingTime;

        DontDestroyOnLoad(this);
    }

    void Update() {
        CalculateAndDisplayTime();
    }

    public void SaveElapsedTime() {
        // Idõ elmentése
        //PlayerPrefs.SetFloat("ElapsedTime", 240f - remainingTime);
        //PlayerPrefs.Save();
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
}
