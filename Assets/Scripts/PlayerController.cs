using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator frontAnimator;
    public Animator rearAnimator;

    // Update is called once per frame
    void Update()
    {
        // Replace this with your logic to check if the player is walking
        bool isWalking = DetermineIfPlayerIsWalking();

        // Replace this with your logic to check if the player is facing front
        bool isFacingFront = DetermineIfPlayerIsFacingFront();

        // Set animation states for front and rear animators
        frontAnimator.SetBool("isWalking", isWalking && isFacingFront);
        rearAnimator.SetBool("isWalking", isWalking && !isFacingFront);

        // Enable the appropriate child based on the facing direction
        frontAnimator.gameObject.SetActive(isFacingFront);
        rearAnimator.gameObject.SetActive(!isFacingFront);
    }

    private bool DetermineIfPlayerIsWalking()
    {
        // Your logic here
        // Example: return Input.GetAxis("Horizontal") != 0;
        return false; // Placeholder
    }

    private bool DetermineIfPlayerIsFacingFront()
    {
        // Your logic here
        // Example: return transform.localScale.x > 0;
        return true; // Placeholder
    }
}
