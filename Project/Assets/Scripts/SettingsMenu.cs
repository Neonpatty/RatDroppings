using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown GraphicsDrop;

    public Toggle FullscreenToggle;

    public Slider EffSlider;

    public Slider MusSlider;

    public AudioMixer EffMixer;

    public AudioMixer MusMixer;

    Resolution[] resolutions;


    float oldValue;

    private void Start()
    {
        resolutions = Screen.resolutions;

        GraphicsDrop.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        GraphicsDrop.AddOptions(options);
        GraphicsDrop.value = currentResolutionIndex;
        GraphicsDrop.RefreshShownValue();

        oldValue = MusSlider.value;
        EffSlider.onValueChanged.AddListener(delegate { SetEffects(); });
        MusSlider.onValueChanged.AddListener(delegate { SetMusic(); });

        MusSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public void SetEffects()
    {
        float EffVolume = EffSlider.value;
        EffMixer.SetFloat("EffectsVol", Mathf.Log10(EffVolume) * 20);

    }

    public void SetMusic()
    {
        float MusVolume = MusSlider.value;
        MusMixer.SetFloat("MusicVol", Mathf.Log10(MusVolume) * 20);

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void FullScreenToggle(bool fullToggle)
    {
        Screen.fullScreen = fullToggle;
    }
}

   
