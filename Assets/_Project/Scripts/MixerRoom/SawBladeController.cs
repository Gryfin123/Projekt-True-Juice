using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeController : MonoBehaviour
{
    [SerializeField] private Sprite _spriteStill; 
    [SerializeField] private Sprite _spriteRunning; 
    [SerializeField] private SpriteRenderer _spriteRendererReference; 
    [SerializeField] private AudioSource _audioSourceReference; 

    [SerializeField] private float _runningSpeed = 0;
    
    private float _speed = 0;
    private bool _isRunning = false;

    public bool Running
    {
        get {return _isRunning;}
        set {
            if (_isRunning != value)
            {
                _isRunning = value;
                if (_isRunning)
                {
                    TurnOn();
                }
                else
                {
                    TurnOff();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    public void ToggleRunning()
    {
        Running = !Running;
    }

    private void TurnOn()
    {
        _speed = _runningSpeed;
        _spriteRendererReference.sprite = _spriteRunning;
        if (_audioSourceReference != null) _audioSourceReference.Play();
    }
    private void TurnOff()
    {
        _speed = 0;
        _spriteRendererReference.sprite = _spriteStill;
        if (_audioSourceReference != null) _audioSourceReference.Stop();
    }

}
