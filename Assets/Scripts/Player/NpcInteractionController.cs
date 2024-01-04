using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractionController : MonoBehaviour
{
    private GameObject interactedNpc;
    private NpcDialogController npcDialogController;
    private PlayerMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        movementController = this.gameObject.GetComponent<PlayerMovementController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("NPC"))
        {
            interactedNpc = collidedGameObject;
            npcDialogController = interactedNpc.GetComponent<NpcDialogController>();
            npcDialogController.ShowNpcDialog();
            movementController.increaseStopTimer();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (npcDialogController)
        {
            npcDialogController.HideNpcDialog();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
