using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,50)] int poolSize = 5;
    [SerializeField] [Range(0.1f,30f)] float spawnTimer = 1f;
    // Start is called before the first frame update

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool(); 
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
