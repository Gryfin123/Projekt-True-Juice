using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Note")]
public class NotesInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _text;
}
