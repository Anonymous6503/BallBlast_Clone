using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private HingeJoint2D[] _wheels;

    private JointMotor2D _motor;

    [SerializeField] private float _canonSpeed;

    private float _screenBounds;

    private Vector2 _pos;
    public float _xVelocity;
    
    public bool ismoving = false;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pos = _rigidbody2D.position;
        _motor = _wheels[0].motor;
        _screenBounds = Game.Instance._screenWidth - 0.56f;
    } 

    // Update is called once per frame
    void Update()
    {
        ismoving = Input.GetMouseButton(0);

        if (ismoving)
        {
            _pos.x = _camera.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

    private void FixedUpdate()
    {
        if (ismoving)
        {
            _rigidbody2D.MovePosition(Vector2.Lerp(_rigidbody2D.position,_pos,_canonSpeed * Time.deltaTime));
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }

        _xVelocity = _rigidbody2D.GetPointVelocity(_rigidbody2D.position).x;

        if (Mathf.Abs(_xVelocity) > 0.0f && Mathf.Abs(_rigidbody2D.position.x) < _screenBounds)
        {
            _motor.motorSpeed = _xVelocity * 150f;
            MotorActivated(true);
        }
        else
        {
            _motor.motorSpeed = 0f;
            MotorActivated(false);
        }
    }

    void MotorActivated(bool isActive)
    {
        _wheels[0].useMotor = isActive;
        _wheels[1].useMotor = isActive;

        _wheels[0].motor = _motor;
        _wheels[1].motor = _motor;
    }
}
