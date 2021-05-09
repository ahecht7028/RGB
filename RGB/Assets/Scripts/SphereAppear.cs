using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spheres in the game will be used for simple puzzle solving, being put into slots and such
public class SphereAppear : MonoBehaviour
{
    MeshRenderer render;
    SphereCollider col;
    public PlayerData.ColorChannel color;
    public bool active;
    //Spheres use a rigidbody so they don't phase through the floor while inactive
    //Platforms don't require this because they are suspended in the air always
    Rigidbody myRig;
    //Allow the player to force the sphere towards them
    CameraController playerCam;
    bool held;
    //Short timer added so player doesn't immediately pick up balls after dropping
    static float timer = 0;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<MeshRenderer>();
        col = gameObject.GetComponent<SphereCollider>();
        myRig = gameObject.GetComponent<Rigidbody>();
        playerCam = FindObjectOfType<CameraController>();
        render.enabled = false;
        col.enabled = false;
        myRig.isKinematic = true;
        active = false;
        held = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Timer for ball being grabbed
        timer -= Time.deltaTime;

        //Drag any non-held balls towards the player if the player is not already holding a ball
        //The range is technically infinite as long as the player is looking in that direction
        //This can both help and hinder level design if kept; the player being able to pick up from long distances will allow them to bring far away balls towards them
        //But if a ball is already in a cup, the player will take the ball from the cup accidentally if the cup is in view
        //This problem can be fixed by adding another boolean that activates when the ball is in a cup, but if the player puts the wrong ball in the wrong cup, they may have to restart the entire level
        if (render.isVisible && Input.GetAxisRaw("Fire1") > 0 && active && !held && !playerCam.holding && timer <= 0)
        {
            float distance = Mathf.Sqrt(Mathf.Pow((transform.position.x - playerCam.transform.position.x), 2) + Mathf.Pow((transform.position.y - playerCam.transform.position.y), 2) + Mathf.Pow((transform.position.z - playerCam.transform.position.z), 2));
            if(distance < 10)
            {
                myRig.position = Vector3.MoveTowards(transform.position, playerCam.transform.position, 0.2f);
            }
        }
        //Keep the held ball a certain distance from the player
        if (held)
        {
            myRig.MovePosition(playerCam.transform.forward * 4 + playerCam.transform.position);
            myRig.useGravity = false;
        }
        //Drop the held ball
        if(held && Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            held = false;
            playerCam.holding = false;
            timer = 1;
            myRig.velocity = new Vector3(0, 0, 0);
            myRig.useGravity = true;
        }
        //The player can drop the ball out of bounds...
        if(transform.position.y < -20)
        {
            held = true;
            playerCam.holding = true;
        }
    }

    //Start being held if we collide with the player
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            held = true;
            playerCam.holding = true;
            audioSource.Play();
        }
    }

    public void UpdateColor(PlayerData.ColorChannel state)
    {
        if (state == color)
        {
            render.enabled = true;
            col.enabled = true;
            myRig.isKinematic = false;
            active = true;
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            render.enabled = false;
            col.enabled = false;
            myRig.isKinematic = true;
            active = false;
        }
    }
}
