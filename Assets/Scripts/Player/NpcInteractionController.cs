using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractionController : MonoBehaviour
{
    public GameObject interactedNpc;
    private CapsuleCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = this.gameObject.GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDED SIMPLE");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDED");
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("NPC"))
        {
            interactedNpc = collidedGameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactedNpc = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
