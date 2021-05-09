using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLetter : MonoBehaviour
{
    public Material defaultColor;
    public Material offColor;
    public MainMenuButton.MenuButton letter;

    MeshRenderer meshRenderer;
    Vector3 pos;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        pos = transform.position;
    }

    public void UpdateOn(MainMenuButton.MenuButton state)
    {
        if(state != letter)
        {
            meshRenderer.material = offColor;
            transform.position = Vector3.MoveTowards(transform.position, pos - new Vector3(0, 2, 0), 50 * Time.deltaTime);
        }
    }

    public void UpdateOff()
    {
        meshRenderer.material = defaultColor;
        transform.position = pos;
    }
}
