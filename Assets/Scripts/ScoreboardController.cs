using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A jegyzetelési panel vezérlõje, amely felelõs a játékos teljesítményének kijelzéséért.
/// </summary>
public class ScoreboardController : MonoBehaviour {
    /// <summary>
    /// A jegyzetelési panel tartalmának kerete.
    /// </summary>
    public RectTransform scoreBoardUiWrapper;

    /// <summary>
    /// A százalékos értéket kijelzõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI percentText;

    /// <summary>
    /// A jegyértéket kijelzõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI gradeText;

    /// <summary>
    /// Az összegyûjtött gyümölcsök számát kijelzõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI fruitText;

    /// <summary>
    /// Az összegyûjtött könyvek számát kijelzõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI bookText;

    /// <summary>
    /// Az eltelt idõt kijelzõ szövegmezõ.
    /// </summary>
    public TextMeshProUGUI timeText;

    /// <summary>
    /// A jegyzetelési panel háttere.
    /// </summary>
    public Image scoreBoardPanel;

    /// <summary>
    /// A megbukott vizsga panel hátterének sprite-ja.
    /// </summary>
    public Sprite failedExamPanelSprite;

    /// <summary>
    /// Az átmenet ideje.
    /// </summary>
    public float transitionDuration = 1.3f;

    /// <summary>
    /// Az osztály példányosításakor hívott metódus.
    /// Beállítja a jegyzetelési panel értékeit és elmozdítja azt a képernyõ közepére animációval.
    /// </summary>
    void Start() {
        SetScoreboardValues();
        scoreBoardUiWrapper.DOMoveY(Screen.height / 2, transitionDuration).SetUpdate(true);
    }

    /// <summary>
    /// Beállítja a jegyzetelési panel értékeit a ScoreManager alapján.
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
    /// Az eltelt idõt formázva visszaadó metódus.
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
    /// Jegy kiszámolását végzõ metódus.
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
    /// Update metódus, amely jelenleg üres.
    /// </summary>
    void Update() {

    }
}
