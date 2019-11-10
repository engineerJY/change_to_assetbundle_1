using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorBuild
{
	[MenuItem("EditorBuild/BuildAndroidAssetBundles")]
	public static void BuildAndroidAssetBundles()
	{
		string assetBundleDirectory = "/Documentation/Build/AssetBundle/ChangeToAssetBundle1/Android";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
	}

    [MenuItem("EditorBuild/BuildiOSAssetBundles")]
    public static void BuildiOSAssetBundles()
    {
        string assetBundleDirectory = "/Documentation/Build/AssetBundle/ChangeToAssetBundle1/iOS";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
    }

    [MenuItem("EditorBuild/BuildWebGLAssetBundles")]
    public static void BuildWebGLAssetBundles()
    {
        string assetBundleDirectory = "/Documentation/Build/AssetBundle/ChangeToAssetBundle1/WebGL";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }

    [MenuItem("EditorBuild/BuildAndroid")]
    public static void BuildAndroid()
    {
        /*
        m_TargetPlatform = BuildTarget.Android;
		bool status = BuildAndroid(m_IsRelease);
		EditorApplication.Exit(status ? 0 : 1);
        */

        Debug.Log("Android Build Start");

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        List<string> allScene = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                Debug.Log(scene.path);
                allScene.Add(scene.path);
            }
        }

        PlayerSettings.applicationIdentifier = "com.EngineerBlog.ChangeToAssetbundle_1";
        PlayerSettings.statusBarHidden = true;
        var a = BuildPipeline.BuildPlayer(
            allScene.ToArray(),
            //"/Documentation/Build/Android/changeToAssetbundle_1.apk",
            Application.dataPath + "Android/changeToAssetbundle_1.apk",
            BuildTarget.Android,
            BuildOptions.None
        );


        Debug.Log("Build End");

    }

}