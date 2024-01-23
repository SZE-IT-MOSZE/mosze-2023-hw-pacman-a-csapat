using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A szint vége eseményeket kezelõ osztály.
/// </summary>
public class LevelEndController : MonoBehaviour {
    /// <summary>
    /// A jeleneteket vezérlõ osztály.
    /// </summary>
    public SceneController sceneController;

    /// <summary>
    /// Kollízió belépés eseménykezelõ.
    /// </summary>
    /// <param name="collision">A kollízióval érintkezõ másik objektum.</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("LevelEndGround")) {
            Time.timeScale = 0;
            StartCoroutine(sceneController.AnimateUIElements(false));
        }
    }

    /// <summary>
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        // Update metódus jelenleg üres.
    }
}
