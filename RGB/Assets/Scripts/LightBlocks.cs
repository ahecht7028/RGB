using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlocks : MonoBehaviour
{
    PlayerData.ColorChannel color;

    public Material red;
    public Material green;
    public Material blue;
    public Material off;

    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = off;
    }

    public void UpdateColor(PlayerData.ColorChannel state)
    {
        switch (state)
        {
            case PlayerData.ColorChannel.Red:
                meshRenderer.material = red;
                break;
            case PlayerData.ColorChannel.Green:
                meshRenderer.material = green;
                break;
            case PlayerData.ColorChannel.Blue:
                meshRenderer.material = blue;
                break;
            case PlayerData.ColorChannel.Off:
                meshRenderer.material = off;
                break;
        }
    }
}
