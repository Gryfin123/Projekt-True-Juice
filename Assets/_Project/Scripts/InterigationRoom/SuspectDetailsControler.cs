using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuspectDetailsControler : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private SuspectInfo _details;


    [Header("Reference")]
    [SerializeField] private TMP_Text _txtName;
    [SerializeField] private TMP_Text _txtBackstory;
    [SerializeField] private TMP_Text _txtCrime;
    [SerializeField] private TMP_Text _txtExcuse;
    
    [SerializeField] private SpriteRenderer _rendererBody;
    [SerializeField] private SpriteRenderer _rendererEyes;
    [SerializeField] private SpriteRenderer _rendererMouth;

    [SerializeField] private CharacterMumbling _characterMumble;

    public SuspectInfo Details
    {
        get {return _details;}
        set 
        {
            _details = value;
            UpdateDisplay();
        }
    }

    public void Start()
    {
        if (_details != null)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        _txtName.text = _details.Name;
        _txtBackstory.text = _details.Backstory;
        _txtCrime.text = _details.Crimes;
        _txtExcuse.text = _details.Excuses;

        _rendererBody.sprite = _details.SpriteBody;
        _rendererEyes.sprite = _details.SpriteEyes;
        _rendererMouth.sprite = _details.SpriteMouth;

        _characterMumble.Pitch = _details.Pitch;
    }
}
