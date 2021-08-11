using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ResourceHelper
{
    public static string LoadDbTextContent(string fileDb)
    {
        string pathDbFile = "DB/" + fileDb;
        TextAsset textAsset = Resources.Load<TextAsset>(pathDbFile);
        if (textAsset != null)
            return textAsset.text;
        else
            return "";
    }

    //public static void EditDbText(string fileDb, string textAsset)
    //{
    //    if (string.IsNullOrEmpty(textAsset))
    //    {
    //        Debug.Log("Text content is null");
    //        return;
    //    }
    //    string pathDbFile = "DB/" + fileDb;
    //    TextAsset txtPath = Resources.Load<TextAsset>(pathDbFile);
    //    File.WriteAllText(AssetDatabase.GetAssetPath(txtPath), textAsset);
    //    EditorUtility.SetDirty(txtPath);
    //}
}
