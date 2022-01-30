using System.Collections.Generic;
using UnityEngine;

public class PaddleColor : MonoBehaviour
{
    public List<MeshRenderer> renderers = new List<MeshRenderer>();

    public Material material;

    public bool onAwake = true;

    private void Awake()
    {
        if (onAwake)
            UpdateMaterials();
    }

    public void UpdateMaterials()
    {
        foreach(MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.material = material;
        }
    }
}
