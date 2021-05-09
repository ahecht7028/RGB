using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public static void ButtonClick(MainMenuButton.MenuButton state)
    {
        switch (state)
        {
            case MainMenuButton.MenuButton.Play:
                PlayButton();
                break;
            case MainMenuButton.MenuButton.Continue:
                ContinueButton();
                break;
            case MainMenuButton.MenuButton.Quit:
                QuitButton();
                break;
        }
    }

    public static void MouseHover(MainMenuButton.MenuButton state)
    {
        MainMenuLetter[] letters = FindObjectsOfType<MainMenuLetter>();
        for(int i = 0; i < letters.Length; i++)
        {
            letters[i].UpdateOn(state);
        }
    }

    public static void MouseExit()
    {
        MainMenuLetter[] letters = FindObjectsOfType<MainMenuLetter>();
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].UpdateOff();
        }
    }

    public static void PlayButton()
    {
        PlayerData.level = 1;
        SceneManager.LoadScene(1);
    }

    public static void ContinueButton()
    {
        PlayerData.level = SaveSystem.LoadLevel();
        SceneManager.LoadScene(PlayerData.level);
    }

    public static void QuitButton()
    {
        Application.Quit();
    }
}
