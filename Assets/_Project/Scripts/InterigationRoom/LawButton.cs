using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LawButton : MonoBehaviour
{
    [SerializeField] private LawInfo _info;

    [Header("References")]
    [SerializeField] private LevelControler _levelControlerReference;
    [SerializeField] private Button _buttonReference;
    [SerializeField] private TMP_Text _titleReference;
    [SerializeField] private TMP_Text _ruleReference;

    public LawInfo Info 
    {
        get {return _info;}
        set 
        {
            _info = value;
            UpdateVisuals();
        }
    }

    public LevelControler LevelControlerReference{
        set {
            if (_levelControlerReference == null) 
            {
                _levelControlerReference = value;
            }
            else
            {
                Debug.Log("It's not possible to change LevelControlerReference in LawButton once it's set.");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (_info != null) UpdateVisuals();
    }
    
    private void UpdateVisuals()
    {
        _titleReference.text = _info.Title;
        _ruleReference.text = _info.Law;

        ColorBlock newColors = new ColorBlock();
        newColors.normalColor = _buttonReference.colors.normalColor;
        newColors.highlightedColor = _info.Punishment.ColorHighlight;
        newColors.pressedColor = _info.Punishment.ColorPressed;
        newColors.selectedColor = _buttonReference.colors.selectedColor;
        newColors.colorMultiplier = 1;
        newColors.fadeDuration = 0.2f;

        _buttonReference.colors = newColors;
    }

    public void OnClick()
    {
        if (_levelControlerReference != null)
        {
            _levelControlerReference.ProposeVerdict(_info);
        }
        else
        {
            Debug.Log("LevelControler is not assigned to LawButton.");
        }
    }
}
