using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

static class ToolbarStyles
{
	public static readonly GUIStyle commandButtonStyle;

	static ToolbarStyles()
	{
		commandButtonStyle = new GUIStyle("Command")
		{
			fontSize = 10,
			alignment = TextAnchor.MiddleCenter,
			imagePosition = ImagePosition.ImageAbove,
			fontStyle = FontStyle.Bold
		};
	}
}
[InitializeOnLoad]
public static class ToolbarExtensions
{
    static ToolbarExtensions() {
        ToolbarExtender.LeftToolbarGUI.Add(DrawButtonPlaynow);
    }
	
    static void DrawButtonPlaynow() {
		GUILayout.FlexibleSpace();
		if (GUILayout.Button(new GUIContent("[▶]", "Dùng để chạy vào login scene từ scene khác"), ToolbarStyles.commandButtonStyle))
		{
			if (Application.isPlaying)
			{
				Debug.Break();
			}
			else {
				SceneHelper.StartScene("HomeScene");
			}
			
		}
	}
	static class SceneHelper
	{
		static string sceneToOpen;

		public static void StartScene(string sceneName)
		{
			if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			sceneToOpen = sceneName;
			EditorApplication.update += OnUpdate;
		}

		static void OnUpdate()
		{
			if (sceneToOpen == null ||
				EditorApplication.isPlaying || EditorApplication.isPaused ||
				EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			EditorApplication.update -= OnUpdate;

			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
				if (guids.Length == 0)
				{
					Debug.LogWarning("Couldn't find scene file");
				}
				else
				{
					string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
					EditorSceneManager.OpenScene(scenePath);
					EditorApplication.isPlaying = true;
				}
			}
			sceneToOpen = null;
		}
	}
}
