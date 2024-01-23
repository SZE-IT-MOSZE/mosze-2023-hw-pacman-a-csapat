using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A játékos animációját vezérlõ osztály.
/// </summary>
public class PlayerAnimationController : MonoBehaviour {
    /// <summary>
    /// A játékos elõnézeti objektum az elülsõ nézetben.
    /// </summary>
    public GameObject FrontPlayerView;

    /// <summary>
    /// A játékos elõnézeti objektum a hátsó nézetben.
    /// </summary>
    public GameObject RearPlayerView;

    /// <summary>
    /// Az animációkat vezérlõ Animator komponens.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// A játékos mozgását vezérlõ osztály.
    /// </summary>
    public PlayerMovementController movementController;

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    private void Start() {
        animator.SetBool("isFront", true);
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }

    /// <summary>
    /// Az animációkat frissítõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        AnimatePlayer();
    }

    /// <summary>
    /// A játékos animációját kezelõ metódus.
    /// </summary>
    private void AnimatePlayer() {
        if (movementController.movement.x != 0 || movementController.movement.y != 0) {
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

            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
        } else if (movementController.movement.x == 0 && movementController.movement.y == 0) {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }
}
