using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public GameObject FrontPlayerView;
    public GameObject RearPlayerView;
    public Animator animator;
    public PlayerMovementController movementController;

    // Update is called once per frame
    void Update()
    {
        AnimatePlayer();
    }

    private void AnimatePlayer()
    {
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
        } else {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }
}