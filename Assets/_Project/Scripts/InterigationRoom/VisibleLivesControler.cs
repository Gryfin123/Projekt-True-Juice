using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleLivesControler : MonoBehaviour
{
    public Image _imageReference;
    public void ChangeVisibleLives(int newAmountLives)
    {   
        _imageReference.rectTransform.sizeDelta = new Vector2(64 * newAmountLives, _imageReference.rectTransform.sizeDelta.y);
    }
}
