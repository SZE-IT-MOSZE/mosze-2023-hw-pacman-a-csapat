using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A jegyzetel�si panel vez�rl�je, amely felel�s a j�t�kos teljes�tm�ny�nek kijelz�s��rt.
/// </summary>
public class ScoreboardController : MonoBehaviour {
    /// <summary>
    /// A jegyzetel�si panel tartalm�nak kerete.
    /// </summary>
    public RectTransform scoreBoardUiWrapper;

    /// <summary>
    /// A sz�zal�kos �rt�ket kijelz� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI percentText;

    /// <summary>
    /// A jegy�rt�ket kijelz� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI gradeText;

    /// <summary>
    /// Az �sszegy�jt�tt gy�m�lcs�k sz�m�t kijelz� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI fruitText;

    /// <summary>
    /// Az �sszegy�jt�tt k�nyvek sz�m�t kijelz� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI bookText;

    /// <summary>
    /// Az eltelt id�t kijelz� sz�vegmez�.
    /// </summary>
    public TextMeshProUGUI timeText;

    /// <summary>
    /// A jegyzetel�si panel h�ttere.
    /// </summary>
    public Image scoreBoardPanel;

    /// <summary>
    /// A megbukott vizsga panel h�tter�nek sprite-ja.
    /// </summary>
    public Sprite failedExamPanelSprite;

    /// <summary>
    /// Az �tmenet ideje.
    /// </summary>
    public float transitionDuration = 1.3f;

    /// <summary>
    /// Az oszt�ly p�ld�nyos�t�sakor h�vott met�dus.
    /// Be�ll�tja a jegyzetel�si panel �rt�keit �s elmozd�tja azt a k�perny� k�zep�re anim�ci�val.
    /// </summary>
    void Start() {
        SetScoreboardValues();
        scoreBoardUiWrapper.DOMoveY(Screen.height / 2, transitionDuration).SetUpdate(true);
    }

    /// <summary>
    /// Be�ll�tja a jegyzetel�si panel �rt�keit a ScoreManager alapj�n.
    /// </summary>
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

        if (grade == 1) {
            scoreBoardPanel.sprite = failedExamPanelSprite;
            percentText.color = new Color32(255, 98, 85, 255);
        }
    }

    /// <summary>
    /// Az eltelt id�t form�zva visszaad� met�dus.
    /// </summary>
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

    /// <summary>
    /// Jegy kisz�mol�s�t v�gz� met�dus.
    /// </summary>
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

    /// <summary>
    /// Update met�dus, amely jelenleg �res.
    /// </summary>
    void Update() {

    }
}
