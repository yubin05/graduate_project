using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    [SerializeField] GameObject[] triggerObjs;   // �� ������Ʈ���� ��� �ı��ǰų� ��Ȱ��ȭ�� ���� �ߵ�

    AudioSource audioSource;
    [SerializeField] AudioClip destroyAudioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    void Start()
    {
        InvokeRepeating("CheckDestroyObjs", 0, 1);
    }

    void CheckDestroyObjs()
    {
        foreach (GameObject triggerObj in triggerObjs)
        {
            if (!(triggerObj == null || triggerObj.activeSelf == false)) return;
        }

        audioSource.clip = destroyAudioClip;
        audioSource.Play();
        // �� �ı�
        Invoke("Destroy", 0.5f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
