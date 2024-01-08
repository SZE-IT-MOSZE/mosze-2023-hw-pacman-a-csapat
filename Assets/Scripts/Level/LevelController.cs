using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int maxNpcSpawnPerType = 4;
    public int maxCollectableFruits = 2;
    public int maxCollectableBooks = 2;
    private int collectedFruitCurrentLevel = 0;
    private int collectedBookCurrentLevel = 0;
    public TextMeshProUGUI fruitText;
    public TextMeshProUGUI bookText;
    public List<GameObject> npcPrefabs = new List<GameObject> { };
    public List<GameObject> fruitPrefabs = new List<GameObject> { };
    public List<GameObject> bookPrefabs = new List<GameObject> { };


    void Start()
    {
        InstantiateNpcPrefabs();
        InstantiateCollectablePrefabs();
        InstantiateCollectableNumbers();
    }

    private void InstantiateNpcPrefabs()
    {
        foreach (GameObject npcPrefab in npcPrefabs)
        {
            for (int i = 0; i < maxNpcSpawnPerType; i++)
            {
                GameObject.Instantiate(npcPrefab);
            }
        }
    }

    private void InstantiateCollectablePrefabs()
    {
        for (int i = 0; i < maxCollectableFruits; i++)
        {
            Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Count)]);
        }

        for (int i = 0; i < maxCollectableBooks; i++)
        {
            Instantiate(bookPrefabs[Random.Range(0, bookPrefabs.Count)]);
        }
    }

    private void InstantiateCollectableNumbers() {
        SetCollectedFruitOnUI(0);
        SetCollectedBookOnUI(0);
        ScoreManager.instance.IncreaseTotalPickupableFruits(maxCollectableFruits);
        ScoreManager.instance.IncreaseTotalPickupableBooks(maxCollectableBooks);
    }

    private void SetCollectedFruitOnUI(int num) {
        fruitText.text = num + "/" + maxCollectableFruits;
    }

    private void SetCollectedBookOnUI(int num) {
        bookText.text = num + "/" + maxCollectableBooks;
    }

    public void AddCollectedFruit() {
        collectedFruitCurrentLevel++;
        SetCollectedFruitOnUI(collectedFruitCurrentLevel);
        ScoreManager.instance.AddCollectedTotalFruit();
    }

    public void AddCollectedBook() {
        collectedBookCurrentLevel++;
        SetCollectedBookOnUI(collectedBookCurrentLevel);
        ScoreManager.instance.AddCollectedTotalBook();
    }

    void Update() {
        
    }
}
