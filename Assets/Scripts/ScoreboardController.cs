using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    public RectTransform scoreBoardUiWrapper;
    public TextMeshProUGUI percentText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI fruitText;
    public TextMeshProUGUI bookText;
    public TextMeshProUGUI timeText;
    public Image scoreBoardPanel;
    public Sprite failedExamPanelSprite;

    public float transitionDuration = 1.3f;

    void Start()
    {
        SetScoreboardValues();
        scoreBoardUiWrapper.DOMoveY(Screen.height / 2, transitionDuration).SetUpdate(true);
    }

    private void SetScoreboardValues() {

        ScoreManager manager = ScoreManager.instance;

        fruitText.text = manager.totalPickupedFruits + "/" + manager.totalPickupableFruits;
        bookText.text = manager.totalPickupedBooks + "/" + manager.totalPickupableBooks;

        float elapsedTime = manager.totalTime - manager.totalTimeLeft;
        timeText.text = FormatElapsedTime(elapsedTime);

        float percent = (float)Math.Round(((float)
            (manager.totalPickupedBooks + manager.totalPickupedFruits) /
            (manager.totalPickupableBooks + manager.totalPickupableFruits)) * 100
        );

        percentText.text = percent + "%";

        int grade = CalculateGrade(percent);
        gradeText.text = "" + grade;

        if(grade == 1) {
            scoreBoardPanel.sprite = failedExamPanelSprite;
            percentText.color = new Color32(255, 98, 85, 255);
        }
    }

    private string FormatElapsedTime(float elapsedTime) {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        string formattedTime;

        if (hours > 0) {
            formattedTime = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        } else {
            formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        return formattedTime;
    }

    private int CalculateGrade(float percent) {
        switch (percent) {
            case float n when n <= 35:
                return 1;
            case float n when n <= 60:
                return 2;
            case float n when n <= 75:
                return 3;
            case float n when n <= 90:
                return 4;
            default:
                return 5;
        }
    }


    void Update()
    {
        
    }
}
