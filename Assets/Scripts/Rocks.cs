using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Random = UnityEngine.Random;

public class Rocks : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rocks;
    [SerializeField] protected int _health;

    [SerializeField] protected TMP_Text _textHealth;

    [SerializeField] protected float _jumpForce;

    protected bool isShowing;
    protected float[] _leftandRight = new float[2]{-1f,1f};

    public bool isResultofSplit = true;
    
    // Start is called before the first frame update
    void Start()
    {
        isShowing = true;
        _rocks.gravityScale = 0f;

        if (isResultofSplit)
        {
            FallDown();
        }
        else
        {
            float direction = _leftandRight[Random.Range(0,1)];
            float _screenWidth = Game.Instance._screenWidth * 1.3f;
            transform.position = new Vector3(_screenWidth * direction, transform.position.y, 0);
            _rocks.velocity = new Vector2(-direction, 0f);
        
            Invoke("FallDown",Random.Range(-_screenWidth-1.5f,_screenWidth-1f));
        }
        
    }

    void FallDown()
    {
        isShowing = false;
        _rocks.gravityScale = 1f;
        _rocks.AddTorque(Random.Range(-20f,20f));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
    }

    void TakeDamage(int _damage)
    {
        if (_health > 1)
        {
            _health -= _damage;
        }
        else
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Canon"))
        {
            Debug.Log("Game Over");
        }
        
        if (other.tag.Equals("Missiles"))
        {
             TakeDamage(1);
             Firing.Instance.DestroyMissile(other.gameObject);
        }
        
        if (!isShowing && other.tag.Equals("Wall"))
        {
            float _posX = transform.position.x;
            if (_posX > 0)
            {
                //RightWall
                _rocks.AddForce(Vector2.left * 150f);
            }
            else
            {
                //LeftWall   
                _rocks.AddForce(Vector2.right * 150f);
            }
            _rocks.AddTorque(_posX * 4f);
        }
        
        if (other.tag.Equals("Ground"))
        {
            _rocks.velocity = new Vector2(_rocks.velocity.x, _jumpForce);
            _rocks.AddTorque(-_rocks.angularVelocity * 4f);
        }
    }

    virtual protected void Death()
    {
        Destroy(gameObject);
    }
    
    protected void UpdateHealthUI()
    {
        _textHealth.text = _health.ToString();
    }
}
