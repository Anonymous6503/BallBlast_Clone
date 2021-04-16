using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _right;
    public float _screenWidth;
    public void Awake()
    {
        _screenWidth = Game.Instance._screenWidth;
        _left.transform.position = new Vector3(-_screenWidth - _left.size.x/2f, 0, 0);
        _right.transform.position = new Vector3(_screenWidth + _right.size.x/ 2f, 0f, 0f);
        Destroy(this);
    }
    
}
