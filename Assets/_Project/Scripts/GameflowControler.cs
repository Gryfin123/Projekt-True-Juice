using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameflowControler : MonoBehaviour
{
    private List<SuspectInfo> _spared;
    private List<SuspectInfo> _executed;

    public List<SuspectInfo> Spared
    {
        get{return _spared;}
    }
    public List<SuspectInfo> Executed
    {
        get{return _executed;}
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _spared = new List<SuspectInfo>();
        _executed = new List<SuspectInfo>();
    }

    public SuspectInfo[] GetSparedSuspects()
    {
        return _spared.ToArray();
    }
    public SuspectInfo[] GetExecutedSuspects()
    {
        return _spared.ToArray();
    }

    public void ResetObject()
    {
        _spared = new List<SuspectInfo>();
        _executed = new List<SuspectInfo>();
    }

    public static GameflowControler FindGameflowController()
    {
        GameObject objGameflowControler = GameObject.FindGameObjectWithTag("GameflowController");
        if (objGameflowControler != null)
        {
            Debug.Log("GameflowController is found");
            return objGameflowControler.GetComponent<GameflowControler>();
        }
        else
        {
            Debug.Log("GameflowController not found");
            return null;
        }
    }

}