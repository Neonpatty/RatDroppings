using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GearDirection
{
    CCW = -1,
    CW = 1,
}

[System.Serializable]
public struct Gear
{
    public Transform mesh;

    public float speed;

    public GearDirection direction;
}

public class GearMover : MonoBehaviour
{
    public List<Gear> gears;

    private void Update()
    {
        foreach (Gear gear in gears)
        {
            gear.mesh.transform.Rotate(0, gear.speed * (int)gear.direction * Time.deltaTime, 0);
        }
    }
}
