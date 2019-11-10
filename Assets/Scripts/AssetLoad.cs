using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetLoad : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadAsset());

        SceneManager.LoadScene("MainScene");
    }

    IEnumerator LoadAsset()
    {
        //UnityWebRequestAssetBundle.GetAssetBundle();

        yield break;
    }
}
