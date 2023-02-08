using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControler : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private LevelInfo _levelInfo;
    public List<SuspectVerdict> _verdicts;
    private SuspectVerdict _currentCase;
    public bool _canClickThings = true;

    [Header("Sounds")]
    [SerializeField] private AudioSource _soundClipFree;
    [SerializeField] private AudioSource _soundClipDeath;
    [SerializeField] private AudioSource _soundClipWrong;

    [Header("Gameplay")]
    [SerializeField] private int _playerLives = 3;
    [SerializeField] private VisibleLivesControler _visibleLivesControlerReference;
    

    [Header("References")]
    [SerializeField] private GameflowControler _gameflowController;

    [SerializeField] private InstantLoadToDifferentScene _sceneLoader;
    [SerializeField] private string _nextSceneSuccess;
    [SerializeField] private string _nextSceneFailure;
    
    [SerializeField] private CharacterMoveScript _suspectMoveScript;
    [SerializeField] private LawController _lawControler;
    [SerializeField] private SuspectDetailsControler _suspectDetailsControler;
    [SerializeField] private MoveToPosition _suspectDetailsMoveScript;
    [SerializeField] private MoveToPosition _lawListMoveScript;


    [Header("Fade in's and out's")]
    [SerializeField] private FadeinOutController _fadeInOutControler;
    [SerializeField] private FadeinOutController _bloodSplatterFadeInOut;
    [SerializeField] private FadeInOutText _bigSubtitleSuccess;
    [SerializeField] private FadeInOutText _bigSubtitleFailure;


    [Header("PunishmentReferences")]
    [SerializeField] private PunishmentInfo _deathPunishment;
    [SerializeField] private PunishmentInfo _freePunishment;



    public SuspectVerdict CurrentCase
    {
        get {return _currentCase;}
        set {
            _currentCase = value;
            UpdateVisibleSuspect();
        }
    }
    private bool CheckIfAllVerdictsAreDone{
        get 
        {
            foreach(SuspectVerdict curr in _verdicts)
            {
                if (!curr.IsJudged)
                {
                    return false;
                }
            }

            return true;
        }
    }
    public int PlayerLives 
    {
        get {return _playerLives;}
        set {
            _playerLives = value;
            _visibleLivesControlerReference.ChangeVisibleLives(_playerLives);

            if (_playerLives <= 0)
            {
                StartCoroutine(ClosingSequenceFailure());
            }
        }
    }

    public void Start()
    {
        InitLevel();
        _gameflowController = GameflowControler.FindGameflowController();
    }


    public void InitLevel()
    {
        LoadLaws();
        LoadSuspects();
        if (_verdicts.Count > 0) SetCurrentSuspect(_verdicts[0].Suspect);
        SpawnSuspect();
    }


    // Load Laws
    public void LoadLaws()
    {
        _lawControler.LawsToDisplay = _levelInfo.Laws;
    }

    // Load Suspects
    public void LoadSuspects()
    {
        _verdicts = new List<SuspectVerdict>();

        foreach(SuspectInfo currSus in _levelInfo.Suspects)
        {
            _verdicts.Add(new SuspectVerdict(currSus));
        }
    }
    public void SetCurrentSuspect(SuspectInfo info)
    {
        foreach(SuspectVerdict curr in _verdicts)
        {
            if (curr.Suspect == info)
            {
                CurrentCase = curr;
                SpawnSuspect();
                return;
            }
        }

        Debug.Log("Suspect not found");
    }
    public bool SetCurrentSuspectToNextAvaiable()
    {
        foreach(SuspectVerdict curr in _verdicts)
        {
            if (curr.Punishment == null)
            {
                CurrentCase = curr;
                SpawnSuspect();
                return true;
            }
        }

        return false;
    }
    public void UpdateVisibleSuspect()
    {
        _suspectDetailsControler.Details = _currentCase.Suspect;
    }


    // Move laws/suspect details (Used for buttons)
    public void ExtendSuspectDetails()
    {
        if (_canClickThings) _suspectDetailsMoveScript._isExtended = true;
    }
    public void RetractSuspectDetails()
    {
        if (_canClickThings) _suspectDetailsMoveScript._isExtended = false;
    }
    public void ExtendLawList()
    {
        if (_canClickThings) _lawListMoveScript._isExtended = true;
    }
    public void RetractLawList()
    {
        if (_canClickThings) _lawListMoveScript._isExtended = false;
    }


    // On Law Click propose verdict
    public void ProposeVerdict(LawInfo judgement)
    {
        // There is no case to judge
        if (_currentCase == null)
        {
            Debug.Log("Case is not set. Verdict can't be applied.");
            return;
        } 

        // Check if player can even judge
        if (!_canClickThings)
        {
            Debug.Log("Judging is currently disabled.");
            return;
        }

        // Check if player made the right decision, go on with the game
        foreach(LawInfo currLaw in _currentCase.Suspect.MatchingLaws)
        {
            if (currLaw == judgement)
            {
                ApplyVerdict(judgement);
                return;
            }
        }

        // ... and if not, notfy him that he has goofed
        WrongLaw();
    }
    public void WrongLaw()
    {
        _soundClipWrong.Play();
        PlayerLives--;
    }
    private void ApplyVerdict(LawInfo judgement)
    {
        _currentCase.Judge(judgement);
        if (judgement.Punishment == _freePunishment)
        {
            if (_gameflowController != null) _gameflowController.Spared.Add(_currentCase.Suspect);
            FreeSuspect();
        }
        if (judgement.Punishment == _deathPunishment)
        {
            if (_gameflowController != null) _gameflowController.Executed.Add(_currentCase.Suspect);
            ExecuteSuspect();
        }

        Debug.Log("Verdict is applied.");
    }

    // If all verdics done, go to different scene
    private void AllVerdictsDone()
    {
        StartCoroutine(ClosingSequenceSuccess());
    }

    // Aesthetics
    private void SpawnSuspect()
    {
        StartCoroutine(SpawnSuspectSequence());
    }
    private IEnumerator SpawnSuspectSequence()
    {
        _canClickThings = false;
        _suspectMoveScript.TeleportToSpawnPosition();
        yield return new WaitForSeconds(0.1f);
        _suspectMoveScript.CurrentTargetState = CurrentDirection.TO_WAITING;
        yield return new WaitForSeconds(1f);
        _suspectDetailsMoveScript._isExtended = true;
        yield return new WaitForSeconds(1.5f);
        _canClickThings = true;
    }
    private void FreeSuspect()
    {
        StartCoroutine(FreeSuspectSequence());
    }
    private IEnumerator FreeSuspectSequence()
    {
        _canClickThings = false;
        _lawListMoveScript._isExtended = false;
        _suspectDetailsMoveScript._isExtended = false;
        yield return new WaitForSeconds(1f);
        _soundClipFree.Play();
        _suspectMoveScript.CurrentTargetState = CurrentDirection.TO_FREE;
        yield return new WaitForSeconds(5f);
        
        if (!CheckIfAllVerdictsAreDone)
        {
            SetCurrentSuspectToNextAvaiable();
        }
        else
        {
            AllVerdictsDone();
        }
    }
    private void ExecuteSuspect()
    {
        StartCoroutine(ExecuteSuspectSequence());
    }
    private IEnumerator ExecuteSuspectSequence()
    {
        _canClickThings = false;
        _lawListMoveScript._isExtended = false;
        _suspectDetailsMoveScript._isExtended = false;
        yield return new WaitForSeconds(1f);
        _soundClipDeath.Play();
        _suspectMoveScript.CurrentTargetState = CurrentDirection.TO_DEATH;
        yield return new WaitForSeconds(3f);
        
        if (!CheckIfAllVerdictsAreDone)
        {
            SetCurrentSuspectToNextAvaiable();
        }
        else
        {
            AllVerdictsDone();
        }
    }

    private IEnumerator ClosingSequenceSuccess()
    {
        _canClickThings = false;
        yield return new WaitForSeconds(2f);
        _bigSubtitleSuccess.FadeOut();
        yield return new WaitForSeconds(1 / _bigSubtitleSuccess.Speed + 1);
        _sceneLoader.LoadSceneFade(_nextSceneSuccess);
    }
    private IEnumerator ClosingSequenceFailure()
    {
        _canClickThings = false;
        _lawListMoveScript._isExtended = false;
        _suspectDetailsMoveScript._isExtended = false;
        yield return new WaitForSeconds(2f);
        _bigSubtitleFailure.FadeOut();
        yield return new WaitForSeconds(1 / _bigSubtitleSuccess.Speed + 1);
        _fadeInOutControler.FadeOut();
        yield return new WaitForSeconds(1 / _fadeInOutControler.Speed);
        // _bloodSplatterFadeInOut.FadeOut();
        // yield return new WaitForSeconds(1 / _bloodSplatterFadeInOut.Speed + 3f);
        _sceneLoader.LoadSceneInstant(_nextSceneFailure);
    }


}

[Serializable]
public class SuspectVerdict
{
    private SuspectInfo _suspect;
    private LawInfo _punishment;

    public SuspectVerdict(SuspectInfo _sus)
    {
        _suspect = _sus;
    }

    public SuspectInfo Suspect{
        get {return _suspect;}
    }
    public LawInfo Punishment{
        get {return _punishment;}
    }

    public bool IsJudged
    {
        get
        {
            if (_punishment != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void Judge(LawInfo _push)
    {
        _punishment = _push;
    }
}


