using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeinOutController : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _fadeCurve;

    private float _current;
    [SerializeField] private float _startingValue = 1;
    [SerializeField] private float _target = 0;


    public float Speed
    {
        get {return _speed;}
    }

    public void Start()
    {
        _current = _startingValue;
    }

    public void Update()
    {
        _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeCurve.Evaluate(_current));
    }
    public void FadeIn()
    {
        _target = 0;
    }
    public void FadeOut()
    {
        _target = 1;
    }
    public void SetCurrent(float yeah)
    {
        _current = yeah;
    }
}
