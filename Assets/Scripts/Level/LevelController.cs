using System.Collections.Generic;
using TMPro;

using UnityEngine;

/// <summary>
/// Az osztály a játékszint vezérléséért felelõs.
/// </summary>
public class LevelController : MonoBehaviour {
    /// <summary>
    /// Az NPC típusonkénti maximális elõfordulási szám.
    /// </summary>
    public int maxNpcSpawnPerType = 4;

    /// <summary>
    /// A szinten található gyûjthetõ gyümölcsök maximális száma.
    /// </summary>
    public int maxCollectableFruits = 2;

    /// <summary>
    /// A szinten található gyûjthetõ könyvek maximális száma.
    /// </summary>
    public int maxCollectableBooks = 2;

    private int collectedFruitCurrentLevel = 0;
    private int collectedBookCurrentLevel = 0;

    /// <summary>
    /// A gyûjthetõ gyümölcsök számát megjelenítõ UI szöveg.
    /// </summary>
    public TextMeshProUGUI fruitText;

    /// <summary>
    /// A gyûjthetõ könyvek számát megjelenítõ UI szöveg.
    /// </summary>
    public TextMeshProUGUI bookText;

    /// <summary>
    /// Az NPC prefab-ok listája, amelyeket a szinten létre kell hozni.
    /// </summary>
    public List<GameObject> npcPrefabs = new List<GameObject> { };

    /// <summary>
    /// A gyümölcs prefab-ok listája, amelyeket a szinten létre kell hozni.
    /// </summary>
    public List<GameObject> fruitPrefabs = new List<GameObject> { };

    /// <summary>
    /// A könyv prefab-ok listája, amelyeket a szinten létre kell hozni.
    /// </summary>
    public List<GameObject> bookPrefabs = new List<GameObject> { };

    /// <summary>
    /// A szint inicializálása, NPC prefab-ok, gyûjthetõ gyümölcsök és könyvek létrehozásával.
    /// </summary>
    void Start() {
        InstantiateNpcPrefabs();
        InstantiateCollectablePrefabs();
        InstantiateCollectableNumbers();
    }

    /// <summary>
    /// NPC prefab-ok létrehozása a megadott típusonkénti maximális elõfordulási szám alapján.
    /// </summary>
    private void InstantiateNpcPrefabs() {
        foreach (GameObject npcPrefab in npcPrefabs) {
            for (int i = 0; i < maxNpcSpawnPerType; i++) {
                GameObject.Instantiate(npcPrefab);
            }
        }
    }

    /// <summary>
    /// Gyûjthetõ gyümölcsök és könyvek létrehozása a megadott maximális számok alapján.
    /// </summary>
    private void InstantiateCollectablePrefabs() {
        for (int i = 0; i < maxCollectableFruits; i++) {
            Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Count)]);
        }

        for (int i = 0; i < maxCollectableBooks; i++) {
            Instantiate(bookPrefabs[Random.Range(0, bookPrefabs.Count)]);
        }
    }

    /// <summary>
    /// Gyûjthetõ gyümölcsök és könyvek számát megjelenítõ UI elemek inicializálása.
    /// </summary>
    private void InstantiateCollectableNumbers() {
        SetCollectedFruitOnUI(0);
        SetCollectedBookOnUI(0);
        ScoreManager.instance.IncreaseTotalPickupableFruits(maxCollectableFruits);
        ScoreManager.instance.IncreaseTotalPickupableBooks(maxCollectableBooks);
    }

    /// <summary>
    /// Frissíti a UI-t a gyûjtött gyümölcsök számának megjelenítéséhez.
    /// </summary>
    /// <param name="num">Az aktuális gyûjtött gyümölcsök száma.</param>
    private void SetCollectedFruitOnUI(int num) {
        fruitText.text = num + "/" + maxCollectableFruits;
    }

    /// <summary>
    /// Frissíti a UI-t a gyûjtött könyvek számának megjelenítéséhez.
    /// </summary>
    /// <param name="num">Az aktuális gyûjtött könyvek száma.</param>
    private void SetCollectedBookOnUI(int num) {
        bookText.text = num + "/" + maxCollectableBooks;
    }

    /// <summary>
    /// Hozzáad egyet a gyûjtött gyümölcsök számához és frissíti a hozzá tartozó UI-t.
    /// </summary>
    public void AddCollectedFruit() {
        collectedFruitCurrentLevel++;
        SetCollectedFruitOnUI(collectedFruitCurrentLevel);
        ScoreManager.instance.AddCollectedTotalFruit();
    }

    /// <summary>
    /// Hozzáad egyet a gyûjtött könyvek számához és frissíti a hozzá tartozó UI-t.
    /// </summary>
    public void AddCollectedBook() {
        collectedBookCurrentLevel++;
        SetCollectedBookOnUI(collectedBookCurrentLevel);
        ScoreManager.instance.AddCollectedTotalBook();
    }

    /// <summary>
    /// A frissítés minden képkockában meghívódik.
    /// </summary>
    void Update() {
        // Jelenleg üres, késõbbi bõvítés céljából.
    }
}
