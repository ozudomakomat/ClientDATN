using UnityEngine;
using System.Collections;

public class PoolObject
{
	public int index { get; set; }

	public string prefabName { get; set; }

	public bool isAvailable { get; set; }

	public GameObject gameObj { get; set; }

	public PoolObject ()
	{
		index = -1;
		prefabName = "";
		isAvailable = false;//true: out of bounds, false: in bounds
		gameObj = null;
	}

	public void recycleObject ()
	{
		index = -1;
		isAvailable = true;
		gameObj.SetActive (false);
	}
}
