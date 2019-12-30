using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetLoad : SingletonMonoBehaviour<AssetLoad>
{
    /// <summary>
    /// ダウンロードURL
    /// </summary>
    const string DOWNLOAD_URL = "http://118.27.18.220/assetbundles/ChangeToAssetBundle1";

    /// <summary>
    /// アセットバンドルの型
    /// </summary>
    public enum ObjectType
    {
        Animation,
        Animator,
        AudioClip,
        Shader,
        Texture,
        Material,
        Model,
        Prefab,
    }

    /// <summary>
    /// Prefabのアセットバンドルを保持
    /// </summary>
    public Dictionary<string, GameObject> assetsPrefabs = new Dictionary<string, GameObject>();
    
    IEnumerator Start()
    {
        //Prefabが参照するアセットバンドルをダウンロード
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/texture.pack", ObjectType.Texture));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/audio.pack", ObjectType.AudioClip));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/shader.pack", ObjectType.Shader));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/material.pack", ObjectType.Material));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/model.pack", ObjectType.Model));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/animation.pack", ObjectType.Animation));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/animator.pack", ObjectType.Animator));

        //Prefabのアセットバンドルをダウンロード
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/prefab.pack", ObjectType.Prefab));

        //アセットバンドルをすべてダウンロード完了したらゲームシーンを加算でロード
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }

    /// <summary>
    /// アセットバンドルをダウンロードし、PrefabはDictionaryに格納
    /// </summary>
    /// <param name="url">ダウンロードURL</param>
    /// <param name="type">アセットバンドルの型</param>
    /// <returns></returns>
    IEnumerator LoadAsset(string url, ObjectType type)
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        request.SendWebRequest();

        while(!request.isDone)
        {
            Debug.Log("wait request");
            yield return null;
        }

        Debug.Log("load done!");

        var assetBundle = DownloadHandlerAssetBundle.GetContent(request);

        switch(type)
        {
            case ObjectType.Animation: break;
            case ObjectType.Animator: break;
            case ObjectType.AudioClip: break;
            case ObjectType.Shader: break;
            case ObjectType.Texture: break;
            case ObjectType.Material: break;
            case ObjectType.Model: break;
                
            case ObjectType.Prefab:
                var assets = assetBundle.LoadAllAssets<GameObject>();
                foreach (var a in assets)
                {
                    Debug.Log(a.name);
                    assetsPrefabs.Add(a.name, a);
                }
                break;
        }
        yield break;
    }
}



/*
 *
 public Dictionary<string, Texture> assetsTextures = new Dictionary<string, Texture>();
    public Dictionary<string, Shader> assetsShaders = new Dictionary<string, Shader>();
    public Dictionary<string, Material> assetsMaterials = new Dictionary<string, Material>();
    public Dictionary<string, Object> assetsObjects = new Dictionary<string, Object>();
    public Dictionary<string, Animation> assetsAnimations = new Dictionary<string, Animation>();
    public Dictionary<string, Animator> assetsAnimators = new Dictionary<string, Animator>();
    public Dictionary<string, AudioClip> assetsAudioClips = new Dictionary<string, AudioClip>();
 */
