using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer MainMixer;
    public Slider Brightnessslider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;
    private void Start()
    {
        brightness.TryGetSettings(out exposure);
        AdjustBrightness(Brightnessslider.value);
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();

        }
    }
    public void SetMusicVolume()
    {
        float Volume=MusicSlider.value;
        MainMixer.SetFloat("Music", Mathf.Log10(Volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", Volume);
    }
    public void SetSFXVolume()
    {
        float Volume = MusicSlider.value;
        MainMixer.SetFloat("SFX", Mathf.Log10(Volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Volume);
    }
    public void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
    }

    public void AdjustBrightness(float value)
    {
        if(value!=0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = .05f;
        }
    }
    //public void SetVolume(float volume)
    //{

        
    //    MainMixer.SetFloat("Volume", volume);
    //}
   public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);

    }
    
  
}
