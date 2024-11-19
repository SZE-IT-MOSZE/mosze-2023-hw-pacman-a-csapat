using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class ScoreManagerTest
{
    private ScoreManager scoreManager = new ScoreManager();

    [Test]
    public void TestIncreasePickupableFruitsNumber() {
        int random = Random.Range(0, 100);

        scoreManager.IncreaseTotalPickupableFruits(random);

        Assert.AreEqual(random, scoreManager.totalPickupableFruits);
    }

    [Test]
    public void TestIncreasePickupableBooksNumber() {
        int random = Random.Range(0, 100);

        scoreManager.IncreaseTotalPickupableBooks(random);

        Assert.AreEqual(random, scoreManager.totalPickupableBooks);
    }

    [Test]
    public void TestAddCollectedFruitsNumber() {

        Assert.AreEqual(0, scoreManager.totalPickupedFruits);

        scoreManager.AddCollectedTotalFruit();

        Assert.AreEqual(1, scoreManager.totalPickupedFruits);
    }

    [Test]
    public void TestAddCollectedBooksNumber() {
        Assert.AreEqual(0, scoreManager.totalPickupedBooks);

        scoreManager.AddCollectedTotalBook();

        Assert.AreEqual(1, scoreManager.totalPickupedBooks);
    }

    [Test]
    public void TestIncreaseTotalTimeValue() {
        float random = Random.Range(0f, 1000f);

        scoreManager.IncreaseTotalTime(random);

        Assert.AreEqual(random, scoreManager.totalTime);
    }

    [Test]
    public void TestIncreaseTotalTimeLeftValue() {
        float random = Random.Range(0f, 1000f);

        scoreManager.IncreaseTotalTimeLeft(random);

        Assert.AreEqual(random, scoreManager.totalTimeLeft);
    }
}
