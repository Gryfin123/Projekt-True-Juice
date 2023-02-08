using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLookDesigner : MonoBehaviour
{
    [Header("Collections")]
    [SerializeField] private SpriteCollection _bodyCollection;
    [SerializeField] private SpriteCollection _eyesCollection;
    [SerializeField] private SpriteCollection _mouthCollection;

    private int _bodyIndex = 0;
    public int BodyIndex{
        get {return _bodyIndex;}
        set 
        {
            _bodyIndex = Mathf.Abs(value % _bodyCollection.Length);
            UpdateBody();
        }
    }
    private int _eyesIndex = 0;
    public int EyesIndex{
        get {return _eyesIndex;}
        set 
        {
            _eyesIndex = Mathf.Abs(value % _eyesCollection.Length);
            UpdateEyes();
        }
    }
    private int _mouthIndex = 0;
    public int MouthIndex{
        get {return _mouthIndex;}
        set 
        {
            _mouthIndex = Mathf.Abs(value % _mouthCollection.Length);
            UpdateMouth();
        }
    }

    [Header("Object References")]
    [SerializeField] private SpriteRenderer _bodyRenderer;
    [SerializeField] private SpriteRenderer _eyesRenderer;
    [SerializeField] private SpriteRenderer _mouthRenderer;


    public void UpdateVisuals()
    {
        UpdateBody();
        UpdateEyes();
        UpdateMouth();
    }
    private void UpdateBody()
    {
        _bodyRenderer.sprite = _bodyCollection[_bodyIndex];
    }
    private void UpdateEyes()
    {
        _eyesRenderer.sprite = _eyesCollection[_eyesIndex];
    }
    private void UpdateMouth()
    {
        _mouthRenderer.sprite = _mouthCollection[_mouthIndex];
    }

    public void NextBody()
    {
        BodyIndex += 1;
    }
    public void PreviousBody()
    {
        if (_bodyIndex - 1 < 0)
        {
            BodyIndex = _bodyCollection.Length - 1;
        }
        else
        {
            BodyIndex -= 1;
        }
    }
    public void NextEyes()
    {
        EyesIndex += 1;
    }
    public void PreviousEyes()
    {
        if (_eyesIndex - 1 < 0)
        {
            EyesIndex = _eyesCollection.Length - 1;
        }
        else
        {
            EyesIndex -= 1;
        }
    }
    public void NextMouth()
    {
        MouthIndex += 1;
    }
    public void PreviousMouth()
    {
        if (_mouthIndex - 1 < 0)
        {
            MouthIndex = _mouthCollection.Length - 1;
        }
        else
        {
            MouthIndex -= 1;
        }
    }
}
