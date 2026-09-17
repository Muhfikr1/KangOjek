using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverCar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _frontTireRB;
    [SerializeField] private Rigidbody2D _backTireRB;
    [SerializeField] private Rigidbody2D _carRb;
    [SerializeField] private float _speed = 150f;
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private AudioSource engineSound; // Tambahkan AudioSource untuk suara mesin
    [SerializeField] private bool _isDead = false; // Flag to check if the player is dead

    private bool _isMovingForward = false;
    private bool _isMovingBackward = false;

    private void Update()
    {
        HandleEngineSound();
    }

    public void StartMoveForward()
    {
        _isMovingForward = true;
        _isMovingBackward = false; 
    }

    public void StopMoveForward()
    {
        _isMovingForward = false;
    }

    public void StartMoveBackward()
    {
        _isMovingBackward = true;
        _isMovingForward = false;
    }

    public void StopMoveBackward()
    {
        _isMovingBackward = false;
    }

    public void SetDead(bool isDead)
    {
        _isDead = isDead;
    }

    private void FixedUpdate()
    {
        if (_isMovingForward)
        {
            _frontTireRB.AddTorque(-_speed * Time.fixedDeltaTime);
            _backTireRB.AddTorque(-_speed * Time.fixedDeltaTime);
            _carRb.AddTorque(_rotationSpeed * Time.fixedDeltaTime);
        }
        else if (_isMovingBackward)
        {
            _frontTireRB.AddTorque(_speed * Time.fixedDeltaTime);
            _backTireRB.AddTorque(_speed * Time.fixedDeltaTime);
            _carRb.AddTorque(-_rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleEngineSound()
    {
        if (_isDead)
        {
            engineSound.Stop(); // Stop the engine sound if the player is dead
            return;
        }

        if (_isMovingForward || _isMovingBackward)
        {
            if (!engineSound.isPlaying) 
            {
                engineSound.Play();
            }
            engineSound.volume = Mathf.Lerp(engineSound.volume, 1f, Time.deltaTime * 5f);
        }
        else
        {
            engineSound.volume = Mathf.Lerp(engineSound.volume, 0f, Time.deltaTime * 5f);
            if (engineSound.volume < 0.05f)
            {
                engineSound.Stop();
            }
        }
    }
}
