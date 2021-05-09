using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangingText : MonoBehaviour
{
    float timer = 1;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.color = new Color(255, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (text.color.r == 255 && timer <= 0)
        {
            timer = 1;
            text.color = new Color(0, 255, 0, 1);
        }
        if (text.color.g == 255 && timer <= 0)
        {
            timer = 1;
            text.color = new Color(0, 0, 255, 1);
        }
        if (text.color.b == 255 && timer <= 0)
        {
            timer = 1;
            text.color = new Color(255, 0, 0, 1);
        }
    }
    
}

