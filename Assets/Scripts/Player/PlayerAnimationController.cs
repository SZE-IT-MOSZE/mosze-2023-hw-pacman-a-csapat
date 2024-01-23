using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A j�t�kos anim�ci�j�t vez�rl� oszt�ly.
/// </summary>
public class PlayerAnimationController : MonoBehaviour {
    /// <summary>
    /// A j�t�kos el�n�zeti objektum az el�ls� n�zetben.
    /// </summary>
    public GameObject FrontPlayerView;

    /// <summary>
    /// A j�t�kos el�n�zeti objektum a h�ts� n�zetben.
    /// </summary>
    public GameObject RearPlayerView;

    /// <summary>
    /// Az anim�ci�kat vez�rl� Animator komponens.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// A j�t�kos mozg�s�t vez�rl� oszt�ly.
    /// </summary>
    public PlayerMovementController movementController;

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    private void Start() {
        animator.SetBool("isFront", true);
        animator.SetBool("isIdle", true);
        animator.SetBool("isWalking", false);
    }

    /// <summary>
    /// Az anim�ci�kat friss�t� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        AnimatePlayer();
    }

    /// <summary>
    /// A j�t�kos anim�ci�j�t kezel� met�dus.
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
