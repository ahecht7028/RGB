using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public enum ColorChannel { Red, Green, Blue, Off };

    public static int level = 1;
    public AudioClip redSound;
    public AudioClip greenSound;
    public AudioClip blueSound;
    public AudioClip deathSound;
    public AudioClip redSong;
    public AudioClip greenSong;
    public AudioClip blueSong;
    public AudioClip pauseSound;
    public AudioClip unpauseSound;

    float timer;
    bool paused;
    bool canPause;
    ColorChannel colorState;
    [HideInInspector]
    public AudioSource audioSource;

    PlayerController playerController;
    CameraController cameraController;
    GameObject pauseMenu;
    GameObject deathMenu;

    void Start()
    {
        timer = 0;
        colorState = ColorChannel.Off;
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        cameraController = transform.Find("Main Camera").GetComponent<CameraController>();
        paused = false;
        pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
        pauseMenu.SetActive(false);
        deathMenu = GameObject.Find("Canvas").transform.Find("DeathScreen").gameObject;
        deathMenu.SetActive(false);
        canPause = true;
    }

    private void Update()
    {
        if (!paused && canPause)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && colorState != ColorChannel.Red)
                {
                    colorState = ColorChannel.Red;
                    UpdateColors();
                    audioSource.PlayOneShot(redSound);
                    ChangeSong(redSong);
                    timer = 1;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && colorState != ColorChannel.Green)
                {
                    colorState = ColorChannel.Green;
                    UpdateColors();
                    audioSource.PlayOneShot(greenSound);
                    ChangeSong(greenSong);
                    timer = 1;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3) && colorState != ColorChannel.Blue)
                {
                    colorState = ColorChannel.Blue;
                    UpdateColors();
                    audioSource.PlayOneShot(blueSound);
                    ChangeSong(blueSong);
                    timer = 1;
                    return;
                }
                if (Input.GetKeyDown(KeyCode.Alpha4) && Input.GetKey(KeyCode.LeftShift) && colorState != ColorChannel.Off)
                {
                    colorState = ColorChannel.Off;
                    UpdateColors();
                    timer = 1;
                    return;
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            // Paused
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }

        if(transform.position.y < -20 && canPause)
        {
            Death();
        }
    }

    public void UpdateColors()
    {
        ColoredBlock[] allColoredObjects = FindObjectsOfType<ColoredBlock>();
        for(int i = 0; i < allColoredObjects.Length; i++)
        {
            allColoredObjects[i].UpdateColor(colorState);
        }

        LightBlocks[] allLightObjects = FindObjectsOfType<LightBlocks>();
        for(int i = 0; i < allLightObjects.Length; i++)
        {
            allLightObjects[i].UpdateColor(colorState);
        }

        SphereAppear[] allSphereObjects = FindObjectsOfType<SphereAppear>();
        for(int i = 0; i < allSphereObjects.Length; i++)
        {
            allSphereObjects[i].UpdateColor(colorState);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        playerController.Pause();
        cameraController.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        pauseMenu.SetActive(true);
        audioSource.PlayOneShot(pauseSound);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        playerController.UnPause();
        cameraController.UnPause();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        pauseMenu.SetActive(false);
        audioSource.PlayOneShot(unpauseSound);
    }

    public void Death()
    {
        Time.timeScale = 0;
        playerController.Pause();
        cameraController.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathMenu.SetActive(true);
        transform.Rotate(new Vector3(0, 0, 10));
        canPause = false;
        audioSource.PlayOneShot(deathSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Death();
        }
    }

    public void ChangeSong(AudioClip song)
    {
        if (audioSource.isPlaying)
        {
            float tempTime = audioSource.time;
            audioSource.clip = song;
            audioSource.Play();
            audioSource.time = tempTime;
        }
        else
        {
            audioSource.clip = song;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<UIAppear>() != null && !other.gameObject.GetComponent<UIAppear>().isTriggered)
        {
            StartCoroutine(other.gameObject.GetComponent<UIAppear>().ActivateUI());
        }
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.GetComponent<ColoredBlock>() != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && collision.gameObject.GetComponent<ColoredBlock>().color == ColorChannel.Red)
            {
                Death();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && collision.gameObject.GetComponent<ColoredBlock>().color == ColorChannel.Green)
            {
                Death();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && collision.gameObject.GetComponent<ColoredBlock>().color == ColorChannel.Blue)
            {
                Death();
            }
        }
    }
    */
}
