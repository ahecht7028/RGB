using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBlock : MonoBehaviour
{
    public PlayerData.ColorChannel color;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    public void UpdateColor(PlayerData.ColorChannel state)
    {
        if(state == color)
        {
            meshRenderer.enabled = true;
            boxCollider.enabled = true;
            transform.parent.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
