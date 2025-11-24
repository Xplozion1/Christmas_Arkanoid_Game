using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
/// <summary>
/// скрипт для регулировки громкости музыки с помощью слайдера
/// </summary>
public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float _value)
    {
        if (_value < 1)
        {
            _value = 0.001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }
    
    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }
    private void RefreshSlider (float _value)
    {
        soundSlider.value = _value;
    }
}
