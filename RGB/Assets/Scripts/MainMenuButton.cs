using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public enum MenuButton { Play, Continue, Quit };
    public MenuButton state;
    public AudioClip hoverSound;
    public AudioClip selectSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    private void OnMouseOver()
    {
        MainMenuBehavior.MouseHover(state);
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(selectSound);
            MainMenuBehavior.ButtonClick(state);
        }
    }

    private void OnMouseExit()
    {
        MainMenuBehavior.MouseExit();
    }
}
