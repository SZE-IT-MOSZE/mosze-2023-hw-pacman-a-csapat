using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementController : MonoBehaviour
{
    public float moveSpeed = 0.35f;
    public Tilemap walkableTilemap;
    public Animator animator;
    public Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 direction = transform.position;
        direction.x = movement.x = Input.GetAxisRaw("Horizontal");
        direction.y = movement.y = (direction.x == 0) ? Input.GetAxisRaw("Vertical") : 0;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        if (!IsWalkable(targetPosition))
        {
            movement.x = 0;
            movement.y = 0;

            return;
        }

        if (movement.y > 0 && movement.x == 0) {
            movement.x = 1.2f;
        } else if (movement.y < 0 && movement.x == 0) {
            movement.x = -1.2f;
        } else if (movement.y == 0 && movement.x != 0) {
            if (animator.GetBool("isFront")) {
                movement.y = -0.6f;
            } else if (animator.GetBool("isRear")) {
                movement.y = 0.6f;
            }
        }

        if (movement.x != 0) {
            transform.localScale = new Vector3(
                Mathf.Sign(movement.x) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }

        transform.position = (Vector2)transform.position + movement * moveSpeed * Time.fixedDeltaTime;
    }

    private bool IsWalkable(Vector2 position)
    {
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        bool hasTile = walkableTilemap.HasTile(cellPosition);

        return hasTile;
    }
}
