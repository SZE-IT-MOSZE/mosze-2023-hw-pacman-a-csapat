using System.Collections;
using UnityEngine;

/// <summary>
/// A pontsz�mkezel� oszt�ly, amely felel�s a j�t�kos teljes�tm�ny�nek k�vet�s��rt.
/// </summary>
public class ScoreManager : MonoBehaviour {
    /// <summary>
    /// Az oszt�ly egyetlen p�ld�nya.
    /// </summary>
    public static ScoreManager instance { get; private set; }

    /// <summary>
    /// Az �sszegy�jt�tt gy�m�lcs�k sz�ma.
    /// </summary>
    public int totalPickupedFruits { get; private set; }

    /// <summary>
    /// Az �sszegy�jt�tt k�nyvek sz�ma.
    /// </summary>
    public int totalPickupedBooks { get; private set; }

    /// <summary>
    /// Az �sszegy�jthet� gy�m�lcs�k maxim�lis sz�ma.
    /// </summary>
    public int totalPickupableFruits { get; private set; }

    /// <summary>
    /// Az �sszegy�jthet� k�nyvek maxim�lis sz�ma.
    /// </summary>
    public int totalPickupableBooks { get; private set; }

    /// <summary>
    /// A teljes j�t�kid�.
    /// </summary>
    public float totalTime { get; private set; }

    /// <summary>
    /// A h�tral�v� j�t�kid�.
    /// </summary>
    public float totalTimeLeft { get; private set; }

    /// <summary>
    /// Az �bred�st kezel� met�dus.
    /// </summary>
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gy�m�lcs hozz�ad�sa az �sszegy�jt�tt gy�m�lcs�k sz�m�hoz.
    /// </summary>
    public void AddCollectedTotalFruit() {
        totalPickupedFruits++;
    }

    /// <summary>
    /// K�nyv hozz�ad�sa az �sszegy�jt�tt k�nyvek sz�m�hoz.
    /// </summary>
    public void AddCollectedTotalBook() {
        totalPickupedBooks++;
    }

    /// <summary>
    /// Az �sszegy�jthet� gy�m�lcs�k maxim�lis sz�m�nak n�vel�se.
    /// </summary>
    public void IncreaseTotalPickupableFruits(int num) {
        totalPickupableFruits += num;
    }

    /// <summary>
    /// Az �sszegy�jthet� k�nyvek maxim�lis sz�m�nak n�vel�se.
    /// </summary>
    public void IncreaseTotalPickupableBooks(int num) {
        totalPickupableBooks += num;
    }

    /// <summary>
    /// Teljes j�t�kid� n�vel�se.
    /// </summary>
    public void IncreaseTotalTime(float num) {
        totalTime += num;
    }

    /// <summary>
    /// H�tral�v� j�t�kid� n�vel�se.
    /// </summary>
    public void IncreaseTotalTimeLeft(float num) {
        totalTimeLeft += num;
    }
}
