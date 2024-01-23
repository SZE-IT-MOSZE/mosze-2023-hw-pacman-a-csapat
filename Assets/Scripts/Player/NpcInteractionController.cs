using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az NPC interakciókat kezelõ osztály.
/// </summary>
public class NpcInteractionController : MonoBehaviour {
    /// <summary>
    /// Az éppen interakcióban lévõ NPC játékobjektuma.
    /// </summary>
    private GameObject interactedNpc;

    /// <summary>
    /// Az NPC párbeszédvezérlõ osztálya.
    /// </summary>
    private NpcDialogController npcDialogController;

    /// <summary>
    /// A játékos mozgását vezérlõ osztály.
    /// </summary>
    private PlayerMovementController movementController;

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    void Start() {
        movementController = this.gameObject.GetComponent<PlayerMovementController>();
    }

    /// <summary>
    /// Kollízió belépés eseménykezelõ.
    /// </summary>
    /// <param name="collision">A kollízióval érintkezõ másik objektum.</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("NPC")) {
            interactedNpc = collidedGameObject;
            npcDialogController = interactedNpc.GetComponent<NpcDialogController>();
            npcDialogController.ShowNpcDialog();
            movementController.IncreaseStopTimer();
        }
    }

    /// <summary>
    /// Kollízió kilépés eseménykezelõ.
    /// </summary>
    /// <param name="collision">A kollízióval érintkezõ másik objektum.</param>
    private void OnCollisionExit2D(Collision2D collision) {
        if (npcDialogController) {
            npcDialogController.HideNpcDialog();
        }
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        // Update metódus jelenleg üres.
    }
}
