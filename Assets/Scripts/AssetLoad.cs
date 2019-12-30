using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetLoad : SingletonMonoBehaviour<AssetLoad>
{
    Dictionary<string, AudioClip> assetsAudioClips = new Dictionary<string, AudioClip>();

    const string DOWNLOAD_URL = "http://118.27.18.220/assetbundles/ChangeToAssetBundle1";
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadAsset());

        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }

    IEnumerator LoadAsset()
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(DOWNLOAD_URL + "/audio.pack");
        request.SendWebRequest();

        while(!request.isDone)
        {
            Debug.Log("wait request");
            yield return null;
        }

        Debug.Log("load done!");

        var assetBundleA = DownloadHandlerAssetBundle.GetContent(request);



        var audioA = assetBundleA.LoadAsset<AudioClip>("SFXHouseAmbience.wav");

        Debug.Log(audioA == null);

        Debug.Log(audioA.name);

        yield break;
    }
}
