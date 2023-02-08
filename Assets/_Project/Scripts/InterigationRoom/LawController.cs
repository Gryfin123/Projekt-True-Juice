using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawController : MonoBehaviour
{
    [SerializeField] private LawInfo[] _lawsToDisplay;
    [SerializeField] private GameObject _lawButtonTemplate;
    [SerializeField] private Transform _targetParentForListObjects;

    [SerializeField] private LevelControler _levelControlerReference;




    public void Start()
    {
        if (_lawsToDisplay != null) InitLawList();
    }

    public LawInfo[] LawsToDisplay
    {
        get {return _lawsToDisplay;}
        set 
        {
            _lawsToDisplay = value;
            InitLawList();
        }
    }

    private void InitLawList()
    {
        foreach(Transform curr in _targetParentForListObjects)
        {
            Destroy(curr.gameObject);
        }

        foreach(LawInfo info in _lawsToDisplay)
        {
            GameObject newButton = Instantiate(_lawButtonTemplate, Vector3.zero, new Quaternion());
            newButton.transform.SetParent(_targetParentForListObjects);
            newButton.transform.localScale = Vector3.one;

            newButton.GetComponent<LawButton>().Info = info;
            newButton.GetComponent<LawButton>().LevelControlerReference = _levelControlerReference;
            
        }
    }


}
