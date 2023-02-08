using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _freePoint;
    [SerializeField] private Transform _deathPoint;

    [SerializeField] private CharacterSpinScript _characterSpinScript;

    [SerializeField] private AnimationCurve _spawnAnimationCurve;
    [SerializeField] private float _spawnAnimationSpeed = 1f;
    [SerializeField] private AnimationCurve _freeAnimationCurve;
    [SerializeField] private float _freeAnimationSpeed = 0.15f;
    [SerializeField] private AnimationCurve _deathAnimationCurve;
    [SerializeField] private float _deathAnimationSpeed = 0.75f;

    private CurrentDirection _currentState;
    public CurrentDirection CurrentTargetState
    {
        get {return _currentState;}
        set
        {
            _currentState = value;

            _current = 0;
            switch(_currentState)
            {
                case CurrentDirection.TO_WAITING:
                    _currentStartPosition = _spawnPoint;
                    _currentTargetPosition = _waitPoint;
                    _currentAnimationCurve = _spawnAnimationCurve;
                    _currentSpeed = _spawnAnimationSpeed;
                    _characterSpinScript._speed = 0;
                break;
                case CurrentDirection.WAITING:
                    _characterSpinScript._speed = 0;
                break;
                case CurrentDirection.TO_FREE:
                    _currentStartPosition = _waitPoint;
                    _currentTargetPosition = _freePoint;
                    _currentAnimationCurve = _freeAnimationCurve;
                    _currentSpeed = _freeAnimationSpeed;
                    _characterSpinScript._speed = 50;
                break;
                case CurrentDirection.TO_DEATH:
                    _currentStartPosition = _waitPoint;
                    _currentTargetPosition = _deathPoint;
                    _currentAnimationCurve = _deathAnimationCurve;
                    _currentSpeed = _deathAnimationSpeed;
                    _characterSpinScript._speed = -120;
                break;
            }
        }
    }

    private Transform _currentStartPosition, _currentTargetPosition;
    private float _currentSpeed;
    private AnimationCurve _currentAnimationCurve;
    private float _current;

    public void Update()
    {
        if (CurrentTargetState != CurrentDirection.WAITING)
        {
            Move();
        }
    }

    public void TeleportToSpawnPosition()
    {
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;
        CurrentTargetState = CurrentDirection.WAITING;
    }

    private void Move()
    {
        _current = Mathf.MoveTowards(_current, 1, _currentSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(_currentStartPosition.position, _currentTargetPosition.position, _currentAnimationCurve.Evaluate(_current));
    }
}

public enum CurrentDirection
{
    WAITING,
    TO_WAITING,
    TO_FREE,
    TO_DEATH,
}
