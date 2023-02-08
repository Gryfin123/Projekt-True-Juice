using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Law")]
public class LawInfo : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private string _law;
    [SerializeField] private PunishmentInfo _punishment;

    public string Title{
        get {return _title;}
    }
    public string Law{
        get {return _law;}
    }
    public PunishmentInfo Punishment{
        get {return _punishment;}
    }
}
