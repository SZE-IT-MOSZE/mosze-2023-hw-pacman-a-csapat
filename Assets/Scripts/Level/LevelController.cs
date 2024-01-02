using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int maxNpcSpawnPerType = 4;
    public List<GameObject> npcPrefabs = new List<GameObject> { };

    // Start is called before the first frame update
    void Start()
    {
        InstantiateNpcPrefabs();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
