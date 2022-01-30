using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sounds/Sound")]
public class Sound : ScriptableObject
{
    public string soundName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public AudioMixerGroup group;
}
