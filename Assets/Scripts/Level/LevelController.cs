using System.Collections.Generic;
using TMPro;

using UnityEngine;

/// <summary>
/// Az oszt�ly a j�t�kszint vez�rl�s��rt felel�s.
/// </summary>
public class LevelController : MonoBehaviour {
    /// <summary>
    /// Az NPC t�pusonk�nti maxim�lis el�fordul�si sz�m.
    /// </summary>
    public int maxNpcSpawnPerType = 4;

    /// <summary>
    /// A szinten tal�lhat� gy�jthet� gy�m�lcs�k maxim�lis sz�ma.
    /// </summary>
    public int maxCollectableFruits = 2;

    /// <summary>
    /// A szinten tal�lhat� gy�jthet� k�nyvek maxim�lis sz�ma.
    /// </summary>
    public int maxCollectableBooks = 2;

    private int collectedFruitCurrentLevel = 0;
    private int collectedBookCurrentLevel = 0;

    /// <summary>
    /// A gy�jthet� gy�m�lcs�k sz�m�t megjelen�t� UI sz�veg.
    /// </summary>
    public TextMeshProUGUI fruitText;

    /// <summary>
    /// A gy�jthet� k�nyvek sz�m�t megjelen�t� UI sz�veg.
    /// </summary>
    public TextMeshProUGUI bookText;

    /// <summary>
    /// Az NPC prefab-ok list�ja, amelyeket a szinten l�tre kell hozni.
    /// </summary>
    public List<GameObject> npcPrefabs = new List<GameObject> { };

    /// <summary>
    /// A gy�m�lcs prefab-ok list�ja, amelyeket a szinten l�tre kell hozni.
    /// </summary>
    public List<GameObject> fruitPrefabs = new List<GameObject> { };

    /// <summary>
    /// A k�nyv prefab-ok list�ja, amelyeket a szinten l�tre kell hozni.
    /// </summary>
    public List<GameObject> bookPrefabs = new List<GameObject> { };

    /// <summary>
    /// A szint inicializ�l�sa, NPC prefab-ok, gy�jthet� gy�m�lcs�k �s k�nyvek l�trehoz�s�val.
    /// </summary>
    void Start() {
        InstantiateNpcPrefabs();
        InstantiateCollectablePrefabs();
        InstantiateCollectableNumbers();
    }

    /// <summary>
    /// NPC prefab-ok l�trehoz�sa a megadott t�pusonk�nti maxim�lis el�fordul�si sz�m alapj�n.
    /// </summary>
    private void InstantiateNpcPrefabs() {
        foreach (GameObject npcPrefab in npcPrefabs) {
            for (int i = 0; i < maxNpcSpawnPerType; i++) {
                GameObject.Instantiate(npcPrefab);
            }
        }
    }

    /// <summary>
    /// Gy�jthet� gy�m�lcs�k �s k�nyvek l�trehoz�sa a megadott maxim�lis sz�mok alapj�n.
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
    /// Gy�jthet� gy�m�lcs�k �s k�nyvek sz�m�t megjelen�t� UI elemek inicializ�l�sa.
    /// </summary>
    private void InstantiateCollectableNumbers() {
        SetCollectedFruitOnUI(0);
        SetCollectedBookOnUI(0);
        ScoreManager.instance.IncreaseTotalPickupableFruits(maxCollectableFruits);
        ScoreManager.instance.IncreaseTotalPickupableBooks(maxCollectableBooks);
    }

    /// <summary>
    /// Friss�ti a UI-t a gy�jt�tt gy�m�lcs�k sz�m�nak megjelen�t�s�hez.
    /// </summary>
    /// <param name="num">Az aktu�lis gy�jt�tt gy�m�lcs�k sz�ma.</param>
    private void SetCollectedFruitOnUI(int num) {
        fruitText.text = num + "/" + maxCollectableFruits;
    }

    /// <summary>
    /// Friss�ti a UI-t a gy�jt�tt k�nyvek sz�m�nak megjelen�t�s�hez.
    /// </summary>
    /// <param name="num">Az aktu�lis gy�jt�tt k�nyvek sz�ma.</param>
    private void SetCollectedBookOnUI(int num) {
        bookText.text = num + "/" + maxCollectableBooks;
    }

    /// <summary>
    /// Hozz�ad egyet a gy�jt�tt gy�m�lcs�k sz�m�hoz �s friss�ti a hozz� tartoz� UI-t.
    /// </summary>
    public void AddCollectedFruit() {
        collectedFruitCurrentLevel++;
        SetCollectedFruitOnUI(collectedFruitCurrentLevel);
        ScoreManager.instance.AddCollectedTotalFruit();
    }

    /// <summary>
    /// Hozz�ad egyet a gy�jt�tt k�nyvek sz�m�hoz �s friss�ti a hozz� tartoz� UI-t.
    /// </summary>
    public void AddCollectedBook() {
        collectedBookCurrentLevel++;
        SetCollectedBookOnUI(collectedBookCurrentLevel);
        ScoreManager.instance.AddCollectedTotalBook();
    }

    /// <summary>
    /// A friss�t�s minden k�pkock�ban megh�v�dik.
    /// </summary>
    void Update() {
        // Jelenleg �res, k�s�bbi b�v�t�s c�lj�b�l.
    }
}
