using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] _rockPrefab;

    [SerializeField] private int _rockCount;

    [SerializeField] private float _spawnDelay;

    private GameObject[] _rocks;

    #region Singleton class: RockSpawner

    public static RockSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    void Start()
    {
        PrepareRocks();
        StartCoroutine(SpawnRocks());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnRocks()
    {
        for (int i = 0; i < _rockCount; i++)
        {
            _rocks[i].SetActive(true);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
    void PrepareRocks()
    {
        _rocks = new GameObject[_rockCount];
        int _prefabCount = _rockPrefab.Length;
        for (int i = 0; i < _rockCount; i++)
        {
            _rocks [i] = Instantiate(_rockPrefab[Random.Range (0,_prefabCount)], transform);
            _rocks[i].GetComponent<Rocks>().isResultofSplit = false;
            _rocks[i].SetActive(false);
        }
    }
}
