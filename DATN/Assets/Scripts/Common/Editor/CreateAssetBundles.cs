using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
	[MenuItem ("MyMenu/Build AssetBundles Win64")]
	static void BuildAllAssetBundlesWin64 ()
	{
		BuildPipeline.BuildAssetBundles ("Exports/Win64", 
			BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
	}

	[MenuItem ("MyMenu/Build AssetBundles OSX")]
	static void BuildAllAssetBundlesOSX ()
	{
		BuildPipeline.BuildAssetBundles ("Exports/OSX", 
			BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
	}

	[MenuItem ("MyMenu/Build AssetBundles Android")]
	static void BuildAllAssetBundlesAndroid ()
	{
		BuildPipeline.BuildAssetBundles ("Exports/Android", 
			BuildAssetBundleOptions.None, BuildTarget.Android);
	}

	[MenuItem ("MyMenu/Build AssetBundles IOS")]
	static void BuildAllAssetBundlesIOS ()
	{
		BuildPipeline.BuildAssetBundles ("Exports/IOS", 
			BuildAssetBundleOptions.None, BuildTarget.iOS);
	}

	[MenuItem ("MyMenu/Get AssetBundle names")]
	static void GetNames ()
	{
		var names = AssetDatabase.GetAllAssetBundleNames();
		foreach (var name in names)
			Debug.Log ("AssetBundle: " + name);
	}
}