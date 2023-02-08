using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScriptForMainMenu : MonoBehaviour
{
    
    [SerializeField] private Transform _currentStartPosition, _currentTargetPosition;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed;
    [SerializeField] private float _startingCurrent;
    private float _current;

    void Start()
    {
        _current = _startingCurrent;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _current = Mathf.MoveTowards(_current, 1, _speed * Time.deltaTime);
        transform.position = Vector3.Lerp(_currentStartPosition.position, _currentTargetPosition.position, _curve.Evaluate(_current));
    
        if (_current == 1)
        {
            _current = 0;
        }
    }
}
