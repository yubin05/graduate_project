using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_BGM : AudioManager
{
    [SerializeField] AudioClip playClip;    // �ش� ������ ����� ���

    protected override void Start()
    {
        base.Start();

        audioSource.clip = playClip;

        audioSource.loop = true;
        audioSource.Play();
    }
}
