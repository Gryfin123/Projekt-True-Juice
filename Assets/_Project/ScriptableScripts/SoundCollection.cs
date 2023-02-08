using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Collections/Sound")]
public class SoundCollection : ScriptableObject
{
    
    [SerializeField] private AudioClip[] _collection;
    [SerializeField] private string _name;
    
    public string Name {
        get{ return _name;}
    }

    public AudioClip this[int x] => FetchSound(x);
    public int Length {
        get { return _collection.Length; }
    }

    private AudioClip FetchSound(int index)
    {
        if (_collection.Length == 0)
        {
            return null;
        }

        if (index < 0 || index >= Length)
        {
            return _collection[0];
        }
        
        return _collection[index];
    }
}
