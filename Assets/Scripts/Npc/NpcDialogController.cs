using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogController : MonoBehaviour
{
    private TimerController timerController;
    private GameObject dialogBubble;
    private NpcMovementController movementController;
    public int secondsPenalty = 10;
    public List<string> dialogs = new List<string> { };
    private System.Random random = new System.Random();

    private void Start()
    {
        timerController = GameObject.Find("GameManager").GetComponent<TimerController>();
        movementController = this.gameObject.GetComponent<NpcMovementController>();
        dialogBubble = this.transform.Find("DialogBubble").gameObject;
        dialogBubble.SetActive(false);
    }

    private void Update()
    {
        Vector3 newScale = dialogBubble.transform.localScale;

        if (this.transform.localScale.x < 0)
        {
            newScale.x = 1;
            dialogBubble.transform.localScale = newScale;
        } else
        {
            newScale.x = -1;
            dialogBubble.transform.localScale = newScale;
        }
    }

    public void ShowNpcDialog()
    {
        int dialogsCount = dialogs.Count;
        int randomDialogIndex = random.Next(0, dialogsCount);
        string randomDialog = dialogs[randomDialogIndex];
        dialogBubble.SetActive(true);
        TextMeshPro dialogText = dialogBubble.transform.Find("DialogText").GetComponent<TextMeshPro>();
        dialogText.text = randomDialog;

        movementController.increaseStopTimer();
        timerController.decreaseTime(secondsPenalty);
    }

    public void HideNpcDialog()
    {
        dialogBubble.SetActive(false);
        this.GetComponent<Animator>().enabled = true;
    }
}
