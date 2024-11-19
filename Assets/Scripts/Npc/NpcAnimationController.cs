using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAnimationController : MonoBehaviour
{
    public GameObject FrontPlayerView;
    public GameObject RearPlayerView;
    private Animator animator;
    private NpcMovementController movementController;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        movementController = this.GetComponent<NpcMovementController>();
        animator.SetBool("isFront", true);
    }

    // Update is called once per frame
    void Update()
    {
        AnimateNpc();
    }

    private void AnimateNpc()
    {
        if (movementController.movement.y > 0)
        {
            RearPlayerView.SetActive(true);
            FrontPlayerView.SetActive(false);
            animator.SetBool("isFront", false);
            animator.SetBool("isRear", true);
        }
        else if (movementController.movement.y < 0)
        {
            RearPlayerView.SetActive(false);
            FrontPlayerView.SetActive(true);
            animator.SetBool("isFront", true);
            animator.SetBool("isRear", false);
        }
    }
}