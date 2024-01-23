using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// Az NPC mozgását vezérlõ osztály.
/// </summary>
public class NpcMovementController : MonoBehaviour {
    /// <summary>
    /// A mozgás irányának eltolása.
    /// </summary>
    private Vector2 moveOffset = new Vector2(1.2f, 0.6f);

    /// <summary>
    /// A mozgási sebesség.
    /// </summary>
    public float moveSpeed = 0.35f;

    /// <summary>
    /// A járható csempetérkép (Tilemap).
    /// </summary>
    private Tilemap walkableTilemap;

    /// <summary>
    /// Az animációkat vezérlõ Animator komponens.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// A mozgás iránya.
    /// </summary>
    public Vector2 movement;

    /// <summary>
    /// A jelenlegi mozgási irány.
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// A véletlen szám generátor.
    /// </summary>
    private System.Random random = new System.Random();

    /// <summary>
    /// Az idõzítõt jelzõ idõ.
    /// </summary>
    public float stopTimerTime = 2;

    /// <summary>
    /// Az aktuális idõzítõ értéke.
    /// </summary>
    public float stopTimer = 0;

    /// <summary>
    /// Az NPC párbeszédvezérlõ osztálya.
    /// </summary>
    private NpcDialogController interactionController;

    /// <summary>
    /// Kezdeti beállításokat végzõ metódus, meghívódik az elsõ képkocka elõtt.
    /// </summary>
    void Start() {
        this.interactionController = this.gameObject.GetComponent<NpcDialogController>();
        walkableTilemap = GameObject.FindGameObjectWithTag("NpcGround").GetComponent<Tilemap>();
        animator = this.GetComponent<Animator>();
        direction = movement = ChooseNewDirection();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2 || currentSceneIndex == 3) {
            moveOffset = new Vector2(1f, 0.5f);
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
    /// A mozgást végzõ metódus.
    /// </summary>
    private void MovePlayer() {
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime;

        if (stopTimer > 0) {
            this.GetComponent<Animator>().enabled = false;
            return;
        }

        if (!IsWalkable(targetPosition)) {
            direction = movement = ChooseNewDirection();
            return;
        }

        if (movement.y > 0 && movement.x == 0) {
            movement.x = moveOffset.x;
        } else if (movement.y < 0 && movement.x == 0) {
            movement.x = -moveOffset.x;
        } else if (movement.y == 0 && movement.x != 0) {
            if (animator.GetBool("isFront")) {
                movement.y = -moveOffset.y;
            } else if (animator.GetBool("isRear")) {
                movement.y = moveOffset.y;
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
    /// Az idõzítõ értékét növelõ metódus.
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
        } else {
            interactionController.HideNpcDialog();
        }
    }

    /// <summary>
    /// Ellenõrzi, hogy a célpozíció járható-e.
    /// </summary>
    /// <param name="position">A célpozíció a világban.</param>
    /// <returns>True, ha a célpozíció járható, egyébként false.</returns>
    private bool IsWalkable(Vector2 position) {
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        return walkableTilemap.HasTile(cellPosition);
    }

    /// <summary>
    /// Véletlenszerûen kiválaszt egy irányt a lehetséges irányok közül.
    /// </summary>
    /// <returns>A kiválasztott irány.</returns>
    private Vector2 ChooseNewDirection() {
        List<Vector2> possibleDirections = new List<Vector2> { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        int index = random.Next(possibleDirections.Count);
        return possibleDirections[index];
    }
}
