using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// Az NPC mozg�s�t vez�rl� oszt�ly.
/// </summary>
public class NpcMovementController : MonoBehaviour {
    /// <summary>
    /// A mozg�s ir�ny�nak eltol�sa.
    /// </summary>
    private Vector2 moveOffset = new Vector2(1.2f, 0.6f);

    /// <summary>
    /// A mozg�si sebess�g.
    /// </summary>
    public float moveSpeed = 0.35f;

    /// <summary>
    /// A j�rhat� csempet�rk�p (Tilemap).
    /// </summary>
    private Tilemap walkableTilemap;

    /// <summary>
    /// Az anim�ci�kat vez�rl� Animator komponens.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// A mozg�s ir�nya.
    /// </summary>
    public Vector2 movement;

    /// <summary>
    /// A jelenlegi mozg�si ir�ny.
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// A v�letlen sz�m gener�tor.
    /// </summary>
    private System.Random random = new System.Random();

    /// <summary>
    /// Az id�z�t�t jelz� id�.
    /// </summary>
    public float stopTimerTime = 2;

    /// <summary>
    /// Az aktu�lis id�z�t� �rt�ke.
    /// </summary>
    public float stopTimer = 0;

    /// <summary>
    /// Az NPC p�rbesz�dvez�rl� oszt�lya.
    /// </summary>
    private NpcDialogController interactionController;

    /// <summary>
    /// Kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
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
    /// Az id�z�t�t kezel� �s friss�t� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    private void Update() {
        HandleStopTimer();
    }

    /// <summary>
    /// A fizikai szimul�ci�k friss�t�sekor megh�v�d� met�dus.
    /// </summary>
    void FixedUpdate() {
        MovePlayer();
    }

    /// <summary>
    /// A mozg�st v�gz� met�dus.
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
    /// Az id�z�t� �rt�k�t n�vel� met�dus.
    /// </summary>
    public void IncreaseStopTimer() {
        stopTimer += stopTimerTime;
    }

    /// <summary>
    /// Az id�z�t�t kezel� met�dus.
    /// </summary>
    private void HandleStopTimer() {
        if (stopTimer >= 0.0f) {
            stopTimer -= Time.deltaTime;
        } else {
            interactionController.HideNpcDialog();
        }
    }

    /// <summary>
    /// Ellen�rzi, hogy a c�lpoz�ci� j�rhat�-e.
    /// </summary>
    /// <param name="position">A c�lpoz�ci� a vil�gban.</param>
    /// <returns>True, ha a c�lpoz�ci� j�rhat�, egy�bk�nt false.</returns>
    private bool IsWalkable(Vector2 position) {
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        return walkableTilemap.HasTile(cellPosition);
    }

    /// <summary>
    /// V�letlenszer�en kiv�laszt egy ir�nyt a lehets�ges ir�nyok k�z�l.
    /// </summary>
    /// <returns>A kiv�lasztott ir�ny.</returns>
    private Vector2 ChooseNewDirection() {
        List<Vector2> possibleDirections = new List<Vector2> { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
        int index = random.Next(possibleDirections.Count);
        return possibleDirections[index];
    }
}
