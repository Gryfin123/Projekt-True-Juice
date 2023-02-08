using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeInOutText : MonoBehaviour
{
    [SerializeField] private TMP_Text _fadeText;
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
        _fadeText.color = new Color(_fadeText.color.r, _fadeText.color.g, _fadeText.color.b, _fadeCurve.Evaluate(_current));
    }
    public void FadeIn()
    {
        _target = 0;
    }
    public void FadeOut()
    {
        _target = 1;
    }
}
