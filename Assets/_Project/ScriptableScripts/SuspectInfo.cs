using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Suspect")]
public class SuspectInfo : ScriptableObject
{
    [Header("Aesthetics")]
    [SerializeField] private Sprite _spriteBody;
    [SerializeField] private Sprite _spriteEyes;
    [SerializeField] private Sprite _spriteMouth;
    [SerializeField] private float _pitch;
    [SerializeField] private GameObject _deathParticleSystemPrefab;

    
    [Header("Personal Details")]
    [SerializeField] private string _name;

    [Header("Lore")]
    [SerializeField] private string _backstory;
    [SerializeField] private string _crimes;
    [SerializeField] private string _excuses;

    [Header("Matching Laws")]
    [SerializeField] private LawInfo[] _matchingLaws;

    public Sprite SpriteBody
    {
        get { return _spriteBody; }
    }
    public Sprite SpriteEyes
    {
        get { return _spriteEyes; }
    }
    public Sprite SpriteMouth
    {
        get { return _spriteMouth; }
    }
    public float Pitch{
        get {return _pitch;}
    }
    public GameObject DeathParticleSystemPrefab{
        get {return _deathParticleSystemPrefab;}
    }


    public string Name{
        get { return _name; }
    }
    public string Backstory{
        get { return _backstory; }
    }
    public string Crimes{
        get { return _crimes; }
    }
    public string Excuses{
        get { return _excuses; }
    }
    public LawInfo[] MatchingLaws{
        get {return _matchingLaws;}
    }






}
