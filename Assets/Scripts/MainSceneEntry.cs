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
    Transform[] wayPoint = null;

    [SerializeField]
    AudioSource audioAmbient = null;

    [SerializeField]
    AudioSource audioWin = null;

    [SerializeField]
    AudioSource audioCaught = null;

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
        public Transform[] wayPoint;

        public InstantiateAsset(string assetName, Vector3 position, Vector3 rotation, Transform parent, Transform[] wayPoint = null)
        {
            this.assetName = assetName;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
            this.wayPoint = wayPoint;
        }
    }

    List<InstantiateAsset> assetList = new List<InstantiateAsset>();

    void Awake()
    {
        Debug.Log("MainSecneEntry Awake");

        audioAmbient.clip = AssetLoad.Instance.assetsAudioClips["SFXHouseAmbience"];
        audioWin.clip = AssetLoad.Instance.assetsAudioClips["SFXWin"];
        audioCaught.clip = AssetLoad.Instance.assetsAudioClips["SFXGameOver"];

        //実体化するPrefabの座標などパラメータ設定

        //プレイアブルキャラクター
        assetList.Add(new InstantiateAsset("JohnLemon", new Vector3(1.254f, -0.7138f, -0.894f), new Vector3(0f, 0f, 0f), parent));

        //ステージ
        assetList.Add(new InstantiateAsset("Level", new Vector3(11.054f, -0.7138002f, 2.306f), new Vector3(0f, 0f, 0f), parent));

        //敵ガーゴイル
        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-15.2f, 0f, 0.8f), new Vector3(0f, 135f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-2.6f, 0f, -8.5f), new Vector3(0f, 30f, 0f), enemyParent));
        assetList.Add(new InstantiateAsset("Gargoyle", new Vector3(-4.8f, 0f, 10.6f), new Vector3(0f, 135f, 0f), enemyParent));

        //敵ゴースト
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(-5.3f, 0f, -3.1f), new Vector3(0f, 0f, 0f), enemyParent, new Transform[]{ wayPoint[0], wayPoint[1] }));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(1.5f, 0f, 4f), new Vector3(0f, 0f, 0f), enemyParent, new Transform[] { wayPoint[2], wayPoint[3] }));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(3.2f, 0f, 6.5f), new Vector3(0f, 0f, 0f), enemyParent, new Transform[] { wayPoint[4], wayPoint[5], wayPoint[6], wayPoint[7] }));
        assetList.Add(new InstantiateAsset("Ghost", new Vector3(7.4f, 0f, -3f), new Vector3(0f, 0f, 0f), enemyParent, new Transform[] { wayPoint[8], wayPoint[9] }));

        //ゲームエンド画像、演出
        assetList.Add(new InstantiateAsset("GameEnding", new Vector3(29.054f, 0.2861998f, 3.806f), new Vector3(0f, 0f, 0f), parent));
        assetList.Add(new InstantiateAsset("FaderCanvas", new Vector3(477.554f, 261.7862f, 2.306f), new Vector3(0f, 0f, 0f), parent));

        //Cinemachineカメラ追随のプレイヤーキャラクターのtransform
        var johnLemon = this.gameObject;

        //Prefabを実体化
        foreach (var asset in assetList)
        {
            var go = Instantiate(AssetLoad.Instance.assetsPrefabs[asset.assetName], asset.position, Quaternion.Euler(asset.rotation), asset.parent);
            go.transform.localPosition = asset.position;


            switch(asset.assetName)
            {
                case "JohnLemon":
                    //JohnLemonのとき、transformを取得
                    johnLemon = go;
                    break;

                case "Ghost":
                    //Ghostのとき、WaypointPatrolにtransform設定
                    go.GetComponent<WaypointPatrol>().SetParam(asset.wayPoint);
                    break;

                case "GameEnding":
                    //GameEndingのとき、パラメーターを設定
                    go.GetComponent<GameEnding>().SetParam(1,1,johnLemon,
                         null, audioWin,
                         null, audioCaught);
                    break;
            }
            
        }

        //Cinemachineのカメラ追随ターゲットにJohnLemonを設定
        virtualCamera.Follow = johnLemon.transform;

        //フェードイン
        fade.gameObject.SetActive(false);
    }
}
