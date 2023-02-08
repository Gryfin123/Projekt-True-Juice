using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SceneIndex")]
public class SceneIndex : ScriptableObject
{
    public Scene _mainMenu;
    public Scene _intro;
    public Scene _mainGameplay;
    public Scene _closingScene;
}
