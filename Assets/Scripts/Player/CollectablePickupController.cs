using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Az �sszegy�jthet� t�rgyak felv�tel�t kezel� oszt�ly.
/// </summary>
public class CollectablePickupController : MonoBehaviour {
    /// <summary>
    /// Koll�zi� bel�p�s esem�nykezel�.
    /// </summary>
    /// <param name="collision">A koll�zi�val �rintkez� m�sik objektum.</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedGameObject = collision.gameObject;

        if (collidedGameObject.CompareTag("FruitCollectable")) {
            Destroy(collision.gameObject);
            GameObject.Find("LevelManager").GetComponent<LevelController>().AddCollectedFruit();
        } else if (collidedGameObject.CompareTag("BookCollectable")) {
            Destroy(collision.gameObject);
            GameObject.Find("LevelManager").GetComponent<LevelController>().AddCollectedBook();
        }
    }

    /// <summary>
    /// Az oszt�ly friss�t�s�t v�gz� met�dus, megh�v�dik minden k�pkock�ban.
    /// </summary>
    void Update() {
        // Update met�dus jelenleg �res.
    }
}
