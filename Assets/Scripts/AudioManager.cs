using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioWalk;
    public AudioClip audioRun;
    public AudioClip audioJump;
    public AudioClip audioLanding;
    public AudioClip audioAttack;
    public AudioClip audioDeath;
    public AudioClip audioHurt;
    public AudioClip audioDash;
    AudioSource audioSource;

    private static AudioClip playAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playAudioClip = null;
    }

    private AudioClip checkAudioName(string audioName)
    {
        AudioClip audioClip;
        if (audioName == "Walk") { audioClip = audioWalk; }
        else if (audioName == "Run") { audioClip = audioRun; }
        else if (audioName == "Jump") { audioClip = audioJump; }
        else if (audioName == "Landing") { audioClip = audioLanding; }
        else if (audioName == "Attack") { audioClip = audioAttack; }
        else if (audioName == "Death") { audioClip = audioDeath; }
        else if (audioName == "Hurt") { audioClip = audioHurt; }
        else if (audioName == "Dash") { audioClip = audioDash; }
        else 
        { 
            audioClip = null;
            Debug.LogError("Can't find audio clip");
        }

        return audioClip;
    }

    public void PlayAudio(string audioName, float volume=1f, float pitch=1f)
    {
        playAudioClip = checkAudioName(audioName);
        audioSource.clip = playAudioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
        playAudioClip = null;
    }

    public void PlayOneShotAudio(string audioName, float volume=1f, float pitch=1f)
    {
        playAudioClip = checkAudioName(audioName);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(playAudioClip, volume);
        playAudioClip = null;

        // revert volume
        audioSource.volume = 1f;
    }
}
