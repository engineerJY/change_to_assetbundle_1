using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetLoad : SingletonMonoBehaviour<AssetLoad>
{
    public Dictionary<string, Texture> assetsTextures = new Dictionary<string, Texture>();
    public Dictionary<string, Shader> assetsShaders = new Dictionary<string, Shader>();
    public Dictionary<string, Material> assetsMaterials = new Dictionary<string, Material>();
    public Dictionary<string, Object> assetsObjects = new Dictionary<string, Object>();
    public Dictionary<string, Animation> assetsAnimations = new Dictionary<string, Animation>();
    public Dictionary<string, Animator> assetsAnimators = new Dictionary<string, Animator>();
    public Dictionary<string, AudioClip> assetsAudioClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, GameObject> assetsPrefabs = new Dictionary<string, GameObject>();

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


    const string DOWNLOAD_URL = "http://118.27.18.220/assetbundles/ChangeToAssetBundle1";
    IEnumerator Start()
    {
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/texture.pack", ObjectType.Texture));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/audio.pack", ObjectType.AudioClip));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/shader.pack", ObjectType.Shader));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/material.pack", ObjectType.Material));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/model.pack", ObjectType.Model));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/animation.pack", ObjectType.Animation));
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/animator.pack", ObjectType.Animator));
        
        yield return StartCoroutine(LoadAsset(DOWNLOAD_URL + "/prefab.pack", ObjectType.Prefab));

        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
    }

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

        var assetBundleA = DownloadHandlerAssetBundle.GetContent(request);

        switch(type)
        {
            case
            ObjectType.Animation:
                var assets = assetBundleA.LoadAllAssets<Animation>();

                foreach (var a in assets)
                {
                    Debug.Log(a.name);
                    assetsAnimations.Add(a.name, a);
                }
                break;

            case
        ObjectType.Animator:
                var assets2 = assetBundleA.LoadAllAssets<Animator>();

                foreach (var a in assets2)
                {
                    Debug.Log(a.name);
                    assetsAnimators.Add(a.name, a);
                }
                break;

            case
        ObjectType.AudioClip:
                break;

            case
        ObjectType.Shader:
                break;

            case
        ObjectType.Texture:
                break;

            case
        ObjectType.Material:
                break;

            case
        ObjectType.Model:
                break;

            case
        ObjectType.Prefab:
                var assets8 = assetBundleA.LoadAllAssets<GameObject>();

                foreach (var a in assets8)
                {
                    Debug.Log(a.name);
                    assetsPrefabs.Add(a.name, a);
                }
                break;

        }


        

        //var audioA = assetBundleA.LoadAsset<AudioClip>("SFXHouseAmbience.wav");

        //Debug.Log(audioA == null);

        //Debug.Log(audioA.name);

        yield break;
    }
}
