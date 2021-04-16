using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RockSplit : Rocks
{

    [SerializeField] private GameObject[] _splitPrefab;

    override protected void Death()
    {
        SplitRock();
        Destroy(gameObject);
    }

    void SplitRock()
    {
        GameObject g;
        for (int i = 0; i < 2; i++)
        {
            g = Instantiate(_splitPrefab[0], transform.position, quaternion.identity);
            g.GetComponent<Rigidbody2D>().velocity = new Vector2(-_leftandRight[i], 5f);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }
}
