using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public SoundsMap soundsMap;

    Dictionary<Sound, AudioSource> sources = new Dictionary<Sound, AudioSource>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        foreach (Sound sound in soundsMap.sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;

            source.volume = sound.volume;
            source.pitch = sound.pitch;

            source.outputAudioMixerGroup = sound.group;

            sources.Add(sound, source);
        }
    }

    public void Play (Sound sound)
    {
        sources[sound].Play();
    }

    public void Play(string name)
    {
        foreach (KeyValuePair<Sound, AudioSource> pair in sources)
        {
            if (pair.Key.soundName.Equals(name))
            {
                pair.Value.Play();
            }
        }
    }
}
