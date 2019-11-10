using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class EditorBuild
{
	[MenuItem("EditorBuild/Build AssetBundles")]
	public static void BuildAllAssetBundles()
	{
		string assetBundleDirectory = "AssetBundlesWebGL";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);

		assetBundleDirectory = "AssetBundlesiOS";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);

		assetBundleDirectory = "AssetBundlesAndroid";
		if (!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}
		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
	}

    public static void _BuildAndroid()
    {
        /*
        m_TargetPlatform = BuildTarget.Android;
		bool status = BuildAndroid(m_IsRelease);
		EditorApplication.Exit(status ? 0 : 1);
        */


        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

        List<string> allScene = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                //Debug.Log(scene.path);
                allScene.Add(scene.path);
            }
        }

        PlayerSettings.applicationIdentifier = "com.EngineerBlog.ChangeToAssetbundle_1";
        PlayerSettings.statusBarHidden = true;
        var a = BuildPipeline.BuildPlayer(
            allScene.ToArray(),
            "/Documentation/Build/Android/changeToAssetbundle_1.apk",
            BuildTarget.Android,
            BuildOptions.None
        );




    }

}