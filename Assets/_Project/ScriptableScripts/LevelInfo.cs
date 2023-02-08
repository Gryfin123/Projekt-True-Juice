using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Level")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private SuspectInfo[] _suspects;
    [SerializeField] private NotesInfo[] _notes;
    [SerializeField] private LawInfo[] _laws;

    public SuspectInfo[] Suspects{
        get {return _suspects;}
    }
    public NotesInfo[] Notes{
        get {return _notes;}
    }
    public LawInfo[] Laws{
        get {return _laws;}
    }
}
