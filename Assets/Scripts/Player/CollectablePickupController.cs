using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectablePickupController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide Pickupable");

        GameObject collidedGameObject = collision.gameObject;

        if (collidedGameObject.CompareTag("FruitCollectable"))
        {
            Destroy(collision.gameObject);
            GameObject.Find("GameManager").GetComponent<LevelController>().AddCollectedFruit();
        } 
        else if(collidedGameObject.CompareTag("BookCollectable"))
        {
            Destroy(collision.gameObject);
            GameObject.Find("GameManager").GetComponent<LevelController>().AddCollectedBook();
        }
    }

    void Update() {

    }
}
