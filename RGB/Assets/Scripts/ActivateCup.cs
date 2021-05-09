using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple code that can be accessed by other codes when needed
public class ActivateCup : MonoBehaviour
{
    public bool activated;
    SphereAppear collidingBall;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        collidingBall = null;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collidingBall != null)
        {
            activated = collidingBall.active;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            activated = true;
            collidingBall = collision.gameObject.GetComponent<SphereAppear>();
            audioSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            activated = false;
            collidingBall = null;
        }
    }
}
