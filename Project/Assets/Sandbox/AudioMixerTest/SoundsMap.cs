using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundsMap", menuName = "Sounds/SoundsMap")]
public class SoundsMap : ScriptableObject
{
    public List<Sound> sounds = new List<Sound>();
}
