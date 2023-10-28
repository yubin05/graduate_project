using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundOptions : MonoBehaviour
{
    // 오디오 믹서
    [SerializeField] public AudioMixer audioMixer;

    // 슬라이더
    [SerializeField] public Slider MasterSlider;
    //public Slider BgmSlider;
    //public Slider SfxSlider;

    private readonly string volumeSliderValueName = "volumeSliderValue";
    //private readonly string audioMixerValueName = "audioMixerValue";
    private void Start()
    {
        if (PlayerPrefs.HasKey(volumeSliderValueName))
        {
            audioMixer.SetFloat("Master", Mathf.Log10(
                PlayerPrefs.GetFloat(volumeSliderValueName)) * 20);
            MasterSlider.value = PlayerPrefs.GetFloat(volumeSliderValueName);
            PlayerPrefs.SetFloat(volumeSliderValueName, MasterSlider.value);
        }
        else
        {
            MasterSlider.value = 1f;
            audioMixer.SetFloat("Master", Mathf.Log10(MasterSlider.value) * 20);
            PlayerPrefs.SetFloat(volumeSliderValueName, MasterSlider.value);
        }
    }

    // 볼륨 조절 - Slider의 OnValueChanged 이벤트에 등록
    public void SetMasterVolme()
    {
        // 로그 연산 값 전달
        audioMixer.SetFloat("Master", Mathf.Log10(
            PlayerPrefs.GetFloat(volumeSliderValueName)) * 20);
        PlayerPrefs.SetFloat(volumeSliderValueName, MasterSlider.value);
    }
    //public void SetBgmVolme()
    //{
    //     // 로그 연산 값 전달
    //    audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);
    //}

    //public void SetSFXVolme()
    //{
    //     // 로그 연산 값 전달
    //    audioMixer.SetFloat("SFX", Mathf.Log10(SfxSlider.value) * 20);
    //}
}