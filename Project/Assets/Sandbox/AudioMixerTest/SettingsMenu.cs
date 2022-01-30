using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace JFSandbox
{
    public class SettingsMenu : MonoBehaviour
    {
        public Dropdown GraphicsDrop;

        public Toggle FullscreenToggle;

        public Slider EffSlider;

        public Slider MusSlider;

        public AudioMixer EffMixer;

        public AudioMixer MusMixer;



        float oldValue;
        private void Start()
        {
            oldValue = MusSlider.value;
            EffSlider.onValueChanged.AddListener(delegate { SetEffects(); });
            MusSlider.onValueChanged.AddListener(delegate { SetMusic(); });

            MusSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
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

        public void SetQuality(int QualityIndex)
        {
            if (GraphicsDrop.value == 0)
                QualitySettings.SetQualityLevel(2);
            if (GraphicsDrop.value == 1)
                QualitySettings.SetQualityLevel(1);
            if (GraphicsDrop.value == 2)
                QualitySettings.SetQualityLevel(0);
        }

    }
}

   
