using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// A j�t�kos mozg�s�t kezel� oszt�ly.
/// </summary>
public class PlayerMovementController : MonoBehaviour {
    /// <summary>
    /// A j�t�kos mozg�si sebess�ge.
    /// </summary>
    public float moveSpeed = 0.35f;

    /// <summary>
    /// A j�rhat� csempet�rk�p (Tilemap).
    /// </summary>
    public Tilemap walkableTilemap;

    /// <summary>
    /// Az anim�ci�kat vez�rl� Animator komponens.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// A j�t�kos mozg�s�nak ir�nya.
    /// </summary>
    public Vector2 movement;

    /// <summary>
    /// Az id�z�t�t jelz� id�.
    /// </summary>
    public float stopTimerTime = 2;

    /// <summary>
    /// Az aktu�lis id�z�t� �rt�ke.
    /// </summary>
    public float stopTimer = 0;

    private float moveOffsetX;
    private float moveOffsetY;

    /// <summary>
    /// A kezdeti be�ll�t�sokat v�gz� met�dus, megh�v�dik az els� k�pkocka el�tt.
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
    /// A j�t�kos mozgat�s�t v�gz� met�dus.
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
    /// Az id�z�t�t n�vel� met�dus.
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
        }
    }

    /// <summary>
    /// Ellen�rzi, hogy a c�lpoz�ci� j�rhat�-e.
    /// </summary>
    /// <param name="position">A c�lpoz�ci� a vil�gban.</param>
    /// <returns>True, ha a c�lpoz�ci� j�rhat�, egy�bk�nt false.</returns>
    private bool IsWalkable(Vector2 position) {
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        bool hasTile = walkableTilemap.HasTile(cellPosition);

        return hasTile;
    }
}
