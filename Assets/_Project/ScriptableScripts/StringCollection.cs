using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Collections/String")]
public class StringCollection : ScriptableObject 
{
    [SerializeField] private string[] _collection;
    [SerializeField] private string _name;

    public string Name {
        get{ return _name;}
    }

    public string this[int x] => FetchString(x);
    public int Length {
        get { return _collection.Length; }
    }

    private string FetchString(int index)
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
