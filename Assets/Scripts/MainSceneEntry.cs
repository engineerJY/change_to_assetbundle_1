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

        //JohnLemon
        var go = Instantiate(AssetLoad.Instance.assetsPrefabs["JohnLemon"]);


        fade.gameObject.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        yield return null;
    }
}
