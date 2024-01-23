using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A szint v�ge esem�nyeket kezel� oszt�ly.
/// </summary>
public class LevelEndController : MonoBehaviour {
    /// <summary>
    /// A jeleneteket vez�rl� oszt�ly.
    /// </summary>
    public SceneController sceneController;

    /// <summary>
    /// Koll�zi� bel�p�s esem�nykezel�.
    /// </summary>
    /// <param name="collision">A koll�zi�val �rintkez� m�sik objektum.</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("LevelEndGround")) {
            Time.timeScale = 0;
            StartCoroutine(sceneController.AnimateUIElements(false));
        }
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        // Update met�dus jelenleg �res.
    }
}
