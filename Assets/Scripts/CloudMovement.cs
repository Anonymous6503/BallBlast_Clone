using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

    [System.Serializable]
    class Clouds
    {
        public MeshRenderer _MeshRenderer = null;
        public float _speed = 0f;
        public Vector2 _offset;
        public Material _material;
    }

    [SerializeField] Clouds[] _allClouds;

    private int _count;
    // Start is called before the first frame update
    void Start()
    {
        _count = _allClouds.Length;
        for (int i = 0; i < _count; i++)
        {
            _allClouds [i]._offset = Vector2.zero;
            _allClouds[i]._material = _allClouds[i]._MeshRenderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _count; i++)
        {
            _allClouds[i]._offset.x += _allClouds[i]._speed * Time.deltaTime;
            _allClouds[i]._material.mainTextureOffset = _allClouds[i]._offset;
        }        
    }
}
