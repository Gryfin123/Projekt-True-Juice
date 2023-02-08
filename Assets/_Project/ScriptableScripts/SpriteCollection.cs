using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Custom Collections/Sprite")]
public class SpriteCollection : ScriptableObject
{
    [SerializeField] private Sprite[] _collection;
    [SerializeField] private string _name;

    public string Name {
        get{ return _name;}
    }

    public Sprite this[int x] => FetchSprite(x);
    public int Length {
        get { return _collection.Length; }
    }

    private Sprite FetchSprite(int index)
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
