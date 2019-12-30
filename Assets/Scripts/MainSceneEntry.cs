using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneEntry : SingletonMonoBehaviour<MainSceneEntry>
{
    [SerializeField]
    Image fade = null;

    void Awake()
    {
        Debug.Log("MainSecneEntry Awake");





        fade.gameObject.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        yield return null;
    }
}
