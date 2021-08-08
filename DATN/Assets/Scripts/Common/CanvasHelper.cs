using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHelper
{
    public static GameObject GetCanvas()
    {
        GameObject goCanvasExpand = GameObject.Find("CanvasExpand");
        //danh cho battle vi chien xa no o tren ca canvas
        GameObject goCanvasTopMost = GameObject.Find("CanvasTopMost");
        //
        if (goCanvasTopMost != null)
        {
            return goCanvasTopMost;
        }
        else if(goCanvasExpand != null)
        {
            return goCanvasExpand;
        }
        else
        { 
            GameObject goCanvas = GameObject.Find("Canvas");
            //
            return goCanvas;
        }
	}
	public static Transform CanvasPopupPanel
	{
		get
		{
			return GameObject.Find("CanvasPopup").transform;
		}
	}
	public static Vector2 GetSize(GameObject go)
	{
		RectTransform rectTf = go.GetComponent<RectTransform> ();
		if (rectTf != null)
		{
			return new Vector2 (rectTf.rect.width, rectTf.rect.height);	
		}
		else
		{
			return Vector2.one;
		}
	}

	public static Vector2 GetPosition(GameObject go)
	{
		RectTransform rectTf = go.GetComponent<RectTransform> ();
		if (rectTf != null)
		{
			return rectTf.anchoredPosition;	
		}
		else
		{
			return Vector2.zero;
		}
	}

    public static void SetPosition(GameObject go, Vector2 position)
	{
		RectTransform rectTf = go.GetComponent<RectTransform> ();
		if (rectTf != null)
		{
			rectTf.anchoredPosition = position;	
		}
	}


	public static void SetPosition(MonoBehaviour mono, Vector2 position)
	{
		RectTransform rectTf = mono.gameObject.GetComponent<RectTransform> ();
		if (rectTf != null)
		{
			rectTf.anchoredPosition = position;	
		}
	}

	public static Vector2 GetPosition(MonoBehaviour mono)
	{
		RectTransform rectTf = mono.gameObject.GetComponent<RectTransform> ();
		if (rectTf != null)
		{
			return rectTf.anchoredPosition;	
		}
		else
		{
			return Vector2.zero;
		}
	}

    public static void ClearChild(GameObject gameObject)
    {
        if (gameObject == null)
            return;
        //
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        //
        gameObject.transform.DetachChildren();
    }


    public static void ClearChild(Transform transform)
	{
		if (transform == null)
			return;
		//
		foreach (Transform child in transform) 
		{
			GameObject.Destroy(child.gameObject);
		}
		//
		transform.DetachChildren ();
	}

	public static void DisableButton(MonoBehaviour obj)
	{
		if (obj == null)
			return;
		Button btn = obj.GetComponent<Button> ();
		if (btn != null)
			btn.interactable = false;
	}

	public static void EnableButton(MonoBehaviour obj)
	{
		if (obj == null)
			return;
		Button btn = obj.GetComponent<Button> ();
		if (btn != null)
			btn.interactable = true;
	}

	public static void DisableButton(GameObject goButton)
	{
		if (goButton == null)
			return;
		Button btn = goButton.GetComponent<Button> ();
		if (btn != null)
			btn.interactable = false;
	}

	public static void EnableButton(GameObject goButton)
	{
		if (goButton == null)
			return;
		Button btn = goButton.GetComponent<Button> ();
		if (btn != null)
			btn.interactable = true;
	}
}
