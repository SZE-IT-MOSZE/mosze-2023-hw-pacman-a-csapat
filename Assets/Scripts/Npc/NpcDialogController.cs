using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Az NPC párbeszédvezérlõ osztály.
/// </summary>
public class NpcDialogController : MonoBehaviour {
    /// <summary>
    /// Az idõmérõ vezérlõ osztály.
    /// </summary>
    private TimerController timerController;

    /// <summary>
    /// A párbeszéd buborék játékobjektum.
    /// </summary>
    private GameObject dialogBubble;

    /// <summary>
    /// Az NPC mozgásvezérlõ osztály.
    /// </summary>
    private NpcMovementController movementController;

    /// <summary>
    /// A büntetendõ idõ másodpercben.
    /// </summary>
    public int secondsPenalty = 10;

    /// <summary>
    /// A rendelkezésre álló párbeszédek listája.
    /// </summary>
    public List<string> dialogs = new List<string> { };

    /// <summary>
    /// A véletlen szám generátor.
    /// </summary>
    private System.Random random = new System.Random();

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    private void Start() {
        timerController = GameObject.Find("LevelManager").GetComponent<TimerController>();
        movementController = this.gameObject.GetComponent<NpcMovementController>();
        dialogBubble = this.transform.Find("DialogBubble").gameObject;
        dialogBubble.SetActive(false);
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    private void Update() {
        // A párbeszéd buborék forgatása az NPC forgatásával együtt.
        Vector3 newScale = dialogBubble.transform.localScale;

        if (this.transform.localScale.x < 0) {
            newScale.x = 1;
            dialogBubble.transform.localScale = newScale;
        } else {
            newScale.x = -1;
            dialogBubble.transform.localScale = newScale;
        }
    }

    /// <summary>
    /// Az NPC párbeszéd megjelenítését kezelõ metódus.
    /// </summary>
    public void ShowNpcDialog() {
        int dialogsCount = dialogs.Count;

        // Véletlenszerûen kiválaszt egy párbeszédet a listából.
        int randomDialogIndex = random.Next(0, dialogsCount);
        string randomDialog = dialogs[randomDialogIndex];

        // Beállítja a kiválasztott párbeszédet a buborék szövegének.
        dialogBubble.SetActive(true);
        TextMeshPro dialogText = dialogBubble.transform.Find("DialogText").GetComponent<TextMeshPro>();
        dialogText.text = randomDialog;

        // Az NPC mozgását leállítja egy rövid ideig.
        movementController.IncreaseStopTimer();
        // Idõlevonást végrehajt a játékos számlapján.
        // timerController.DecreaseTime(secondsPenalty);
    }

    /// <summary>
    /// Az NPC párbeszéd elrejtését kezelõ metódus.
    /// </summary>
    public void HideNpcDialog() {
        // A párbeszéd buborék elrejtése.
        dialogBubble.SetActive(false);
        // Az NPC animációja újra engedélyezése.
        this.GetComponent<Animator>().enabled = true;
    }
}
