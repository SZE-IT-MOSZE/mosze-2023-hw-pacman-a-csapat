using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Az összegyûjthetõ tárgyak felvételét kezelõ osztály.
/// </summary>
public class CollectablePickupController : MonoBehaviour {
    /// <summary>
    /// Kollízió belépés eseménykezelõ.
    /// </summary>
    /// <param name="collision">A kollízióval érintkezõ másik objektum.</param>
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
    /// Az osztály frissítését végzõ metódus, meghívódik minden képkockában.
    /// </summary>
    void Update() {
        // Update metódus jelenleg üres.
    }
}
