using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;

public class Firing : MonoBehaviour
{
    private Queue<GameObject> _missileQueue;

    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private int _missileCount;

    [Space] 
    [SerializeField] private float delay = .3f;
    [SerializeField] private float speed = .3f;

    private GameObject _missileClone;

    private float t = 0f;

    #region Singleton class: Instance;
    public static Firing Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PrepareMissile();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t>=delay)
        {
            t = 0f;
            _missileClone = SpawnMissile(transform.position);
            _missileClone.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }
    }
    
    void PrepareMissile()
    {
        _missileQueue = new Queue<GameObject>();
        for (int i = 0; i < _missileCount; i++)
        {
            _missileClone = Instantiate(_missilePrefab, transform.position, Quaternion.identity,transform);
            _missileClone.SetActive(false);
            _missileQueue.Enqueue(_missileClone);
        }
    }

    GameObject SpawnMissile(Vector2 pos)
    {
        if (_missileQueue.Count > 0)
        {
            _missileClone = _missileQueue.Dequeue();
            _missileClone.transform.position = pos;
            _missileClone.SetActive(true);
            return _missileClone;
        }
        else
        {
            return null;
        }
    }

    public void DestroyMissile(GameObject _missile)
    {
        _missileQueue.Enqueue(_missile);
        _missile.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Missiles"))
        {
            DestroyMissile(other.gameObject);
        }
    }
}
