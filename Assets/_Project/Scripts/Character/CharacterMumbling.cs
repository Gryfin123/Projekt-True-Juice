using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMumbling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSrc;
    [SerializeField] private SoundCollection _mumbles;
    [SerializeField] private float _pitch = 1;
    [SerializeField] private float _playDelay = 5;
    [SerializeField] private float _playDelayMin = 10;
    [SerializeField] private float _playDelayMax = 20;
    
    
    public float Pitch{
        get {return _pitch;}
        set 
        {
            _pitch = value;
            _audioSrc.pitch = _pitch;
        }
    }

    void Start()
    {
        _audioSrc.pitch = _pitch;
        InvokeRepeating("PlayMumble", _playDelay, Random.Range(_playDelayMin, _playDelayMax));
    }

    private void PlayMumble()
    {
        AudioClip playMe = GetRandomClip();
        _audioSrc.clip = playMe;
        _audioSrc.Play();
    }

    public void RandomPitch()
    {
        _audioSrc.pitch = Random.Range(0f, 2f);
    }

    private AudioClip GetRandomClip()
    {
        int randomNum = Random.Range(0, _mumbles.Length);
        return _mumbles[randomNum];
    }
}
