using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Az NPC anim�ci�kat vez�rl� oszt�ly.
/// </summary>
public class NpcAnimationController : MonoBehaviour {
    /// <summary>
    /// A j�t�kos el�ls� n�zet objektuma.
    /// </summary>
    public GameObject FrontPlayerView;

    /// <summary>
    /// A j�t�kos h�ts� n�zet objektuma.
    /// </summary>
    public GameObject RearPlayerView;

    /// <summary>
    /// Az anim�tor komponens.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Az NPC mozg�svez�rl� oszt�ly.
    /// </summary>
    private NpcMovementController movementController;

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
    /// </summary>
    private void Start() {
        animator = this.GetComponent<Animator>();
        movementController = this.GetComponent<NpcMovementController>();
        animator.SetBool("isFront", true);
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        AnimateNpc();
    }

    /// <summary>
    /// Az NPC anim�ci�j�t kezel� met�dus.
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
