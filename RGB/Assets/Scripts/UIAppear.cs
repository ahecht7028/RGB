using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAppear : MonoBehaviour
{
    public GameObject UIElement;

    [HideInInspector]
    public bool isTriggered;

    void Start()
    {
        UIElement.SetActive(false);
        isTriggered = false;
    }

    public IEnumerator ActivateUI()
    {
        isTriggered = true;
        UIElement.SetActive(true);
        yield return new WaitForSeconds(5);
        UIElement.SetActive(false);
    }
}
