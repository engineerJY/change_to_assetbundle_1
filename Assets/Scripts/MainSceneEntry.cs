using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneEntry : SingletonMonoBehaviour<MainSceneEntry>
{
    [SerializeField]
    Transform parent = null;

    [SerializeField]
    Transform enemyParent = null;

    [SerializeField]
    Cinemachine.CinemachineVirtualCamera virtualCamera = null;

    [SerializeField]
    Image fade = null;

    /// <summary>
    /// 実体化するPrefabの配置情報クラス
    /// </summary>
    class InstantiateAsset
    {
        public string assetName;
        public Vector3 position;
        public Vector3 rotation;
        public Transform parent;

        public InstantiateAsset(string assetName, Vector3 position, Vector3 rotation, Transform parent)
        {
            this.assetName = assetName;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
        }
    }

    List<InstantiateAsset> assetList = new List<InstantiateAsset>();

    void Awake()
    {
        Debug.Log("MainSecneEntry Awake");

        //実体化するPrefabの座標などを設定
        assetList.Add(new InstantiateAsset("JohnLemon", new Vector3(1.254f, -0.7138f, -0.894f), new Vector3(0f, 0f, 0f), parent));

        assetList.Add(new InstantiateAsset("Level", new Vector3(11.054f, -0.7138002f, 2.306f), new Vector3(0f, 0f, 0f), parent));

        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-15.2f, 0f, 0.8f), new Vector3(0f, 135f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-2.6f, 0f, -8.5f), new Vector3(0f, 30f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-4.8f, 0f, 10.6f), new Vector3(0f, 135f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(-5.3f, 0f, -3.1f), new Vector3(0f, 0f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(1.5f, 0f, 4f), new Vector3(0f, 0f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(3.2f, 0f, 6.5f), new Vector3(0f, 0f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(7.4f, 0f, -3f), new Vector3(0f, 0f, 0f), enemyParent));

        assetList.Add(new InstantiateAsset("GameEnding", new Vector3(29.054f, 0.2861998f, 3.806f), new Vector3(0f, 0f, 0f), parent));
        assetList.Add(new InstantiateAsset("FaderCanvas", new Vector3(477.554f, 261.7862f, 2.306f), new Vector3(0f, 0f, 0f), parent));

        var johnLemonTransform = this.transform;
        //Prefabを実体化
        foreach (var asset in assetList)
        {
            var go = Instantiate(AssetLoad.Instance.assetsPrefabs[asset.assetName], asset.position, Quaternion.Euler(asset.rotation), asset.parent);
            go.transform.localPosition = asset.position;
            
            //JohnLemonのとき、transformを取得
            if(asset.assetName == "JohnLemon")
            {
                johnLemonTransform = go.transform;
            }
        }

        //Cinemachineのカメラ追随ターゲットにJohnLemonを設定
        virtualCamera.Follow = johnLemonTransform;

        //フェードイン
        fade.gameObject.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        yield return null;
    }
}
