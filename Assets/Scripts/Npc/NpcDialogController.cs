using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Az NPC p�rbesz�dvez�rl� oszt�ly.
/// </summary>
public class NpcDialogController : MonoBehaviour {
    /// <summary>
    /// Az id�m�r� vez�rl� oszt�ly.
    /// </summary>
    private TimerController timerController;

    /// <summary>
    /// A p�rbesz�d bubor�k j�t�kobjektum.
    /// </summary>
    private GameObject dialogBubble;

    /// <summary>
    /// Az NPC mozg�svez�rl� oszt�ly.
    /// </summary>
    private NpcMovementController movementController;

    /// <summary>
    /// A b�ntetend� id� m�sodpercben.
    /// </summary>
    public int secondsPenalty = 10;

    /// <summary>
    /// A rendelkez�sre �ll� p�rbesz�dek list�ja.
    /// </summary>
    public List<string> dialogs = new List<string> { };

    /// <summary>
    /// A v�letlen sz�m gener�tor.
    /// </summary>
    private System.Random random = new System.Random();

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    private void Start() {
        timerController = GameObject.Find("LevelManager").GetComponent<TimerController>();
        movementController = this.gameObject.GetComponent<NpcMovementController>();
        dialogBubble = this.transform.Find("DialogBubble").gameObject;
        dialogBubble.SetActive(false);
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    private void Update() {
        // A p�rbesz�d bubor�k forgat�sa az NPC forgat�s�val egy�tt.
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
    /// Az NPC p�rbesz�d megjelen�t�s�t kezel� met�dus.
    /// </summary>
    public void ShowNpcDialog() {
        int dialogsCount = dialogs.Count;

        // V�letlenszer�en kiv�laszt egy p�rbesz�det a list�b�l.
        int randomDialogIndex = random.Next(0, dialogsCount);
        string randomDialog = dialogs[randomDialogIndex];

        // Be�ll�tja a kiv�lasztott p�rbesz�det a bubor�k sz�veg�nek.
        dialogBubble.SetActive(true);
        TextMeshPro dialogText = dialogBubble.transform.Find("DialogText").GetComponent<TextMeshPro>();
        dialogText.text = randomDialog;

        // Az NPC mozg�s�t le�ll�tja egy r�vid ideig.
        movementController.IncreaseStopTimer();
        // Id�levon�st v�grehajt a j�t�kos sz�mlapj�n.
        // timerController.DecreaseTime(secondsPenalty);
    }

    /// <summary>
    /// Az NPC p�rbesz�d elrejt�s�t kezel� met�dus.
    /// </summary>
    public void HideNpcDialog() {
        // A p�rbesz�d bubor�k elrejt�se.
        dialogBubble.SetActive(false);
        // Az NPC anim�ci�ja �jra enged�lyez�se.
        this.GetComponent<Animator>().enabled = true;
    }
}
