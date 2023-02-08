using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MixerRoomController : MonoBehaviour
{
    [Header("Essential")]
    [SerializeField] private List<SuspectInfo> _spared;
    [SerializeField] private List<SuspectInfo> _executed;
    private GameflowControler _gameflowController;
    private SuspectInfo _currentSuspect;
    public SuspectInfo CurrentSuspect{
        get {return _currentSuspect;}
        set 
        {
            _currentSuspect = value;
            _suspectDetailsController.Details = _currentSuspect;
        }
    }

    [Header("References")]
    [SerializeField] private SawBladeController[] _sawController; 
    [SerializeField] private SuspectDetailsControler _suspectDetailsController; 
    [SerializeField] private CharacterMoveScript _characterMoveScript; 
    [SerializeField] private GameObject _objExecutionButton; 
    [SerializeField] private TMP_Text _objExecutionButtonText; 
    [SerializeField] private InstantLoadToDifferentScene _sceneLoader; 
    [SerializeField] private string _targetSceneWhenDone; 

    [Header("Ending Texts")]
    [SerializeField] private FadeInOutText _sparedText;
    [SerializeField] private FadeInOutText _executedText;
    [SerializeField] private FadeinOutController _fadeInOutController;
    [SerializeField] private FadeinOutController _splatterController;
    [SerializeField] private ParticleSystem _splatterParticleSystem; 

    private bool _executedWerePresent = false;

    [Header("Liquid In Glass")]
    private float _maxSuspects, _currentAmountExecuted;
    [SerializeField] private Image _liquidImage; 

    // Start is called before the first frame update
    void Start()
    {
        if (_spared == null) _spared = new List<SuspectInfo>();
        if (_executed == null) _executed = new List<SuspectInfo>();
        _gameflowController = GameflowControler.FindGameflowController();
        if (_gameflowController != null)
        {
            FetchDataFromGameflowController();
        }
        
        if (_executed.Count > 0)
        {
            _executedWerePresent = true;
        }
        
        _maxSuspects = _spared.Count + _executed.Count;
        _currentAmountExecuted = 0;
        Reevaluate();
    }

    private void FetchDataFromGameflowController()
    {
        _spared = _gameflowController.Spared;
        _executed = _gameflowController.Executed;
        _gameflowController.ResetObject();
    }

    private void Reevaluate()
    {
        // if there are executed
        if (_executed.Count > 0)
        {
            ShowExecutionButton();
        }
        // if there is no more executed
        else if (_executed.Count <= 0 && _executedWerePresent)
        {
            StartExecutionDoneTextSequence();
        }
        // if there is no more executed and never was
        else if (_executed.Count <= 0 && !_executedWerePresent)
        {
            StartCupRemainsCleanSequence();
        }
    }

    private void UpdateLiquidInGlass()
    {
        _liquidImage.fillAmount = _currentAmountExecuted / _maxSuspects;
    }

    private void ShowExecutionButton()
    {
        _objExecutionButton.SetActive(true);
    }
    private void HideExecutionButton()
    {
        _objExecutionButton.SetActive(false);
    }

    private void StartExecutionDoneTextSequence()
    {
        StartCoroutine(ExecutionDoneTextSequence());
    }
    private IEnumerator ExecutionDoneTextSequence()
    {
        yield return new WaitForSeconds(1f);
        _executedText.FadeOut();
        yield return new WaitForSeconds(1 / _executedText.Speed + 3f);
        _fadeInOutController.FadeOut();
        yield return new WaitForSeconds(1 / _fadeInOutController.Speed + 2f);
        _sceneLoader.LoadSceneInstant(_targetSceneWhenDone);
    }

    private void StartCupRemainsCleanSequence()
    {
        StartCoroutine(CupRemainsCleanSequence());
    }
    private IEnumerator CupRemainsCleanSequence()
    {
        yield return new WaitForSeconds(5f);
        _sparedText.FadeOut();
        yield return new WaitForSeconds(1 / _sparedText.Speed + 3f);
        _fadeInOutController.FadeOut();
        yield return new WaitForSeconds(1 / _fadeInOutController.Speed + 2f);
        _sceneLoader.LoadSceneInstant(_targetSceneWhenDone);
    }

    public void StartExecutionSequence()
    {
        StartCoroutine(ExecutionSequence());
    }
    private IEnumerator ExecutionSequence()
    {
        CurrentSuspect = _executed[0];
        HideExecutionButton();
        _objExecutionButtonText.text = "Next One";
        yield return new WaitForSeconds(1f);
        foreach(SawBladeController _curr in _sawController)
        {
            _curr.Running = true;
        }
        yield return new WaitForSeconds(2f);
        _characterMoveScript.CurrentTargetState = CurrentDirection.TO_DEATH;

        yield return new WaitForSeconds(1.5f);
        _splatterParticleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        _characterMoveScript.TeleportToSpawnPosition();
        _currentAmountExecuted++;
        // splatter + squash sound
        _splatterController.SetCurrent(0);
        _splatterController.FadeOut();
        UpdateLiquidInGlass();
        _executed.RemoveAt(0);
        yield return new WaitForSeconds(2f);
        foreach(SawBladeController _curr in _sawController)
        {
            _curr.Running = false;
        }
        yield return new WaitForSeconds(2f);
    
        Reevaluate();
    }


}
