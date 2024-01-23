using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az NPC animációkat vezérlõ osztály.
/// </summary>
public class NpcAnimationController : MonoBehaviour {
    /// <summary>
    /// A játékos elülsõ nézet objektuma.
    /// </summary>
    public GameObject FrontPlayerView;

    /// <summary>
    /// A játékos hátsó nézet objektuma.
    /// </summary>
    public GameObject RearPlayerView;

    /// <summary>
    /// Az animátor komponens.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Az NPC mozgásvezérlõ osztály.
    /// </summary>
    private NpcMovementController movementController;

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    private void Start() {
        animator = this.GetComponent<Animator>();
        movementController = this.GetComponent<NpcMovementController>();
        animator.SetBool("isFront", true);
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        AnimateNpc();
    }

    /// <summary>
    /// Az NPC animációját kezelõ metódus.
    /// </summary>
    private void AnimateNpc() {
        if (movementController.movement.y > 0) {
            RearPlayerView.SetActive(true);
            FrontPlayerView.SetActive(false);
            animator.SetBool("isFront", false);
            animator.SetBool("isRear", true);
        } else if (movementController.movement.y < 0) {
            RearPlayerView.SetActive(false);
            FrontPlayerView.SetActive(true);
            animator.SetBool("isFront", true);
            animator.SetBool("isRear", false);
        }
    }
}
