using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{
    public SceneController sceneController;

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.CompareTag("LevelEndGround")) {
            Time.timeScale = 0;
            StartCoroutine(sceneController.AnimateUIElements(false));
        }
    }

    void Update() {

    }
}
