using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Player : AudioManager
{
    public AudioClip audioWalk;
    public AudioClip audioRun;
    public AudioClip audioJump;
    public AudioClip audioLanding;
    public AudioClip audioAttack;
    public AudioClip audioDeath;
    public AudioClip audioHurt;
    public AudioClip audioDash;

    protected override AudioClip checkAudioName(string audioName)
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
}
