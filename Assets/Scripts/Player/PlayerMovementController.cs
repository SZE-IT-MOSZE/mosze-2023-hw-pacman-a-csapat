using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// A játékos mozgását kezelõ osztály.
/// </summary>
public class PlayerMovementController : MonoBehaviour {
    /// <summary>
    /// A játékos mozgási sebessége.
    /// </summary>
    public float moveSpeed = 0.35f;

    /// <summary>
    /// A járható csempetérkép (Tilemap).
    /// </summary>
    public Tilemap walkableTilemap;

    /// <summary>
    /// Az animációkat vezérlõ Animator komponens.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// A játékos mozgásának iránya.
    /// </summary>
    public Vector2 movement;

    /// <summary>
    /// Az idõzítõt jelzõ idõ.
    /// </summary>
    public float stopTimerTime = 2;

    /// <summary>
    /// Az aktuális idõzítõ értéke.
    /// </summary>
    public float stopTimer = 0;

    private float moveOffsetX;
    private float moveOffsetY;

    /// <summary>
    /// A kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    void Start() {
        walkableTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2 || currentSceneIndex == 3) {
            moveOffsetX = 1f;
            moveOffsetY = .5f;
        } else {
            moveOffsetX = 1.2f;
            moveOffsetY = .6f;
        }
    }

    /// <summary>
    /// Az idõzítõt kezelõ és frissítõ metódus, meghívódik minden képkockában.
    /// </summary>
    private void Update() {
        HandleStopTimer();
    }

    /// <summary>
    /// A fizikai szimulációk frissítésekor meghívódó metódus.
    /// </summary>
    void FixedUpdate() {
        MovePlayer();
    }

    /// <summary>
    /// A játékos mozgatását végzõ metódus.
    /// </summary>
    private void MovePlayer() {
        Vector2 direction = transform.position;
        direction.x = movement.x = Input.GetAxisRaw("Horizontal");
        direction.y = movement.y = (direction.x == 0) ? Input.GetAxisRaw("Vertical") : 0;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        if (!IsWalkable(targetPosition) || stopTimer > 0) {
            movement.x = 0;
            movement.y = 0;

            return;
        }

        if (movement.y > 0 && movement.x == 0) {
            movement.x = moveOffsetX;
        } else if (movement.y < 0 && movement.x == 0) {
            movement.x = -moveOffsetX;
        } else if (movement.y == 0 && movement.x != 0) {
            if (animator.GetBool("isFront")) {
                movement.y = -moveOffsetY;
            } else if (animator.GetBool("isRear")) {
                movement.y = moveOffsetY;
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

    /// <summary>
    /// Az idõzítõt növelõ metódus.
    /// </summary>
    public void IncreaseStopTimer() {
        stopTimer += stopTimerTime;
    }

    /// <summary>
    /// Az idõzítõt kezelõ metódus.
    /// </summary>
    private void HandleStopTimer() {
        if (stopTimer >= 0.0f) {
            stopTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Ellenõrzi, hogy a célpozíció járható-e.
    /// </summary>
    /// <param name="position">A célpozíció a világban.</param>
    /// <returns>True, ha a célpozíció járható, egyébként false.</returns>
    private bool IsWalkable(Vector2 position) {
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        bool hasTile = walkableTilemap.HasTile(cellPosition);

        return hasTile;
    }
}
