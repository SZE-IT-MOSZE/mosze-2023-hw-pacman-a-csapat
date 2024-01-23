using System.Collections;
using UnityEngine;

/// <summary>
/// A pontszámkezelõ osztály, amely felelõs a játékos teljesítményének követéséért.
/// </summary>
public class ScoreManager : MonoBehaviour {
    /// <summary>
    /// Az osztály egyetlen példánya.
    /// </summary>
    public static ScoreManager instance { get; private set; }

    /// <summary>
    /// Az összegyûjtött gyümölcsök száma.
    /// </summary>
    public int totalPickupedFruits { get; private set; }

    /// <summary>
    /// Az összegyûjtött könyvek száma.
    /// </summary>
    public int totalPickupedBooks { get; private set; }

    /// <summary>
    /// Az összegyûjthetõ gyümölcsök maximális száma.
    /// </summary>
    public int totalPickupableFruits { get; private set; }

    /// <summary>
    /// Az összegyûjthetõ könyvek maximális száma.
    /// </summary>
    public int totalPickupableBooks { get; private set; }

    /// <summary>
    /// A teljes játékidõ.
    /// </summary>
    public float totalTime { get; private set; }

    /// <summary>
    /// A hátralévõ játékidõ.
    /// </summary>
    public float totalTimeLeft { get; private set; }

    /// <summary>
    /// Az ébredést kezelõ metódus.
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
    /// Gyümölcs hozzáadása az összegyûjtött gyümölcsök számához.
    /// </summary>
    public void AddCollectedTotalFruit() {
        totalPickupedFruits++;
    }

    /// <summary>
    /// Könyv hozzáadása az összegyûjtött könyvek számához.
    /// </summary>
    public void AddCollectedTotalBook() {
        totalPickupedBooks++;
    }

    /// <summary>
    /// Az összegyûjthetõ gyümölcsök maximális számának növelése.
    /// </summary>
    public void IncreaseTotalPickupableFruits(int num) {
        totalPickupableFruits += num;
    }

    /// <summary>
    /// Az összegyûjthetõ könyvek maximális számának növelése.
    /// </summary>
    public void IncreaseTotalPickupableBooks(int num) {
        totalPickupableBooks += num;
    }

    /// <summary>
    /// Teljes játékidõ növelése.
    /// </summary>
    public void IncreaseTotalTime(float num) {
        totalTime += num;
    }

    /// <summary>
    /// Hátralévõ játékidõ növelése.
    /// </summary>
    public void IncreaseTotalTimeLeft(float num) {
        totalTimeLeft += num;
    }
}
