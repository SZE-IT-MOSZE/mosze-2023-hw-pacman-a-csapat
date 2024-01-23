using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az NPC interakci�kat kezel� oszt�ly.
/// </summary>
public class NpcInteractionController : MonoBehaviour {
    /// <summary>
    /// Az �ppen interakci�ban l�v� NPC j�t�kobjektuma.
    /// </summary>
    private GameObject interactedNpc;

    /// <summary>
    /// Az NPC p�rbesz�dvez�rl� oszt�lya.
    /// </summary>
    private NpcDialogController npcDialogController;

    /// <summary>
    /// A j�t�kos mozg�s�t vez�rl� oszt�ly.
    /// </summary>
    private PlayerMovementController movementController;

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    void Start() {
        movementController = this.gameObject.GetComponent<PlayerMovementController>();
    }

    /// <summary>
    /// Koll�zi� bel�p�s esem�nykezel�.
    /// </summary>
    /// <param name="collision">A koll�zi�val �rintkez� m�sik objektum.</param>
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
    /// Koll�zi� kil�p�s esem�nykezel�.
    /// </summary>
    /// <param name="collision">A koll�zi�val �rintkez� m�sik objektum.</param>
    private void OnCollisionExit2D(Collision2D collision) {
        if (npcDialogController) {
            npcDialogController.HideNpcDialog();
        }
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        // Update met�dus jelenleg �res.
    }
}
