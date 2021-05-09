using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public AudioClip jumpSound;

    public bool canJump;
    bool paused;
    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        speed = 8;
        jumpHeight = 800;
        canJump = false;
        paused = false;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!paused)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            transform.Translate(x, 0, z);
            if (Input.GetKeyDown(KeyCode.Space) && canJump && (int)rb.velocity.y == 0)
            {
                canJump = false;
                rb.AddForce(new Vector3(0, jumpHeight, 0));
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if(col.gameObject.CompareTag("Platform"))
        {
            canJump = true;
            //transform.position = col.gameObject.transform.position;
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            canJump = true;
        }
        if (col.gameObject.CompareTag("Platform"))
        {
            canJump = true;
            //transform.position = col.gameObject.transform.position;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            canJump = false;
        }
        if (col.gameObject.tag == "Platform")
        {
            transform.parent = null;
            canJump = false;
        }
    }

    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
    }
}
