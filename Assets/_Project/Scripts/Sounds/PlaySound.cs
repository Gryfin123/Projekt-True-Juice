using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void PlaySoundClip(AudioClip _snd)
    {
        AudioSource.PlayClipAtPoint(_snd, transform.position);
    }
}
