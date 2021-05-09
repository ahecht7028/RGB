using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    ActivateCup button;
    Rigidbody myRig;
    public Vector3 goal;
    Vector3 start;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.transform.parent.Find("pCylinder1").GetComponent<ActivateCup>();
        myRig = gameObject.GetComponent<Rigidbody>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (button.activated)
        {
            myRig.MovePosition(Vector3.MoveTowards(transform.position, goal, speed));
        }
        else
        {
            myRig.MovePosition(Vector3.MoveTowards(transform.position, start, speed));
        }
    }
}
