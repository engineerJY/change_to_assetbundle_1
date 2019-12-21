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
        var request = UnityWebRequestAssetBundle.GetAssetBundle("http://118.27.18.220/assets/texture.pack");
        request.SendWebRequest();

        while(!request.isDone)
        {
            Debug.Log("wait request");
            yield return null;
        }


        var assetBundleA = DownloadHandlerAssetBundle.GetContent(request);

        yield break;
    }
}
