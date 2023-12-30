using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcMovementController : MonoBehaviour
{
    private Vector2 moveOffset = new Vector2(1.2f, 0.6f);
    public float moveSpeed = 0.35f;
    public Tilemap walkableTilemap;
    private Animator animator;

    public Vector2 movement;
    public Vector2 direction;
    private System.Random random = new System.Random();

    void Start()
    {
        animator = this.GetComponent<Animator>();
        direction = movement = ChooseNewDirection();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        if (!IsWalkable(targetPosition))
        {
            direction = movement = ChooseNewDirection();
            return;
        }

        if (movement.y > 0 && movement.x == 0)
        {
            movement.x = moveOffset.x;
        }
        else if (movement.y < 0 && movement.x == 0)
        {
            movement.x = -moveOffset.x;
        }
        else if (movement.y == 0 && movement.x != 0)
        {
            if (animator.GetBool("isFront"))
            {
                movement.y = -moveOffset.y;
            }
            else if (animator.GetBool("isRear"))
            {
                movement.y = moveOffset.y;
            }
        }

        if (movement.x != 0)
        {
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
        return walkableTilemap.HasTile(cellPosition);
    }

    private Vector2 ChooseNewDirection()
    {
        List<Vector2> possibleDirections = new List<Vector2> { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

        int index = random.Next(possibleDirections.Count);
        return possibleDirections[index];
    }
}
