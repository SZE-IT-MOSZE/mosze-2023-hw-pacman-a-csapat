using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    public int totalPickupedFruits { get; private set; }
    public int totalPickupedBooks { get; private set; }
    public int totalPickupableFruits { get; private set; }
    public int totalPickupableBooks { get; private set; }
    public float totalTime { get; private set; }
    public float totalTimeLeft { get; private set; }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddCollectedTotalFruit() {
        totalPickupedFruits++;
    }

    public void AddCollectedTotalBook() {
        totalPickupedBooks++;
    }

    public void IncreaseTotalPickupableFruits(int num) {
        totalPickupableFruits += num;
    }

    public void IncreaseTotalPickupableBooks(int num) {
        totalPickupableBooks += num;
    }

    public void IncreaseTotalTime(float num) {
        totalTime += num;
    }

    public void IncreaseTotalTimeLeft(float num) {
        totalTimeLeft += num;
    }
}
