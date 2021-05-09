using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public AudioClip victorySound;
    bool win;

    private void Start()
    {
        win = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!win && collision.gameObject.tag == "Player")
        {
            win = true;
            StartCoroutine(FinishLevel(collision.gameObject.GetComponent<PlayerData>()));
        }
    }

    IEnumerator FinishLevel(PlayerData player)
    {
        player.audioSource.Stop();
        player.audioSource.PlayOneShot(victorySound);
        yield return new WaitForSeconds(4);
        PlayerData.level++;
        SaveSystem.SaveLevel();
        SceneManager.LoadScene(PlayerData.level);
    }
}
