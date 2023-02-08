using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    public Transform _retractedPoint, _extendedPoint;
    public bool _isExtended;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed = 0.5f;
    private float _current, _target /* set to 1 if object is supposed to move */;

    // Update is called once per frame
    void Update()
    {
        if (_retractedPoint != null && _extendedPoint != null)
        {
            _target = _isExtended == false ? 0 : 1;

            _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);

            transform.position = Vector3.Lerp(_retractedPoint.position, _extendedPoint.position, _curve.Evaluate(_current));
        }
    }
}
