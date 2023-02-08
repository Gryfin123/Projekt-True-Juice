using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Punishment")]
public class PunishmentInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _text;
    [SerializeField] private Color _colorHighlight;
    [SerializeField] private Color _colorPressed;

    
    public string Nmae{
        get {return _name;}
    }
    public string Text{
        get {return _text;}
    }
    public Color ColorHighlight{
        get {return _colorHighlight;}
    }
    public Color ColorPressed{
        get {return _colorPressed;}
    }
}
