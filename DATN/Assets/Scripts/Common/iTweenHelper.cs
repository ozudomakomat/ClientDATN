using UnityEngine;
using System.Collections;

public class iTweenHelper 
{
	public static void MoveGameObject(GameObject go, Vector2 to, float duration, 
		iTween.EaseType easeType = iTween.EaseType.linear)
	{
		Vector3 posNew = new Vector3(to.x, to.y, 0);
		//
		Hashtable htb = new Hashtable ();
		htb.Add ("position", posNew);
		htb.Add ("time", duration);
		htb.Add ("islocal", true);
		htb.Add ("ignoretimescale", true);
		//htb.Add ("oncomplete", "MoveCompleted");
		htb.Add ("easetype", easeType);
		//
		iTween.MoveTo(go,htb);
	}

	public static void MakeScale(GameObject go, float scaleFrom, float scaleTo, float duration,
		iTween.EaseType easeType = iTween.EaseType.linear)
	{
		go.transform.localScale = new Vector3(scaleFrom,scaleFrom,scaleFrom);
		//
		Hashtable htb = new Hashtable ();
		htb.Add ("scale", new Vector3(scaleTo,scaleTo,scaleTo));
		htb.Add ("time", duration);
		htb.Add ("islocal", true);
		htb.Add ("ignoretimescale", true);
		htb.Add ("easetype", easeType);
		//
		iTween.ScaleTo(go,htb);
	}


	//
    public static void MakeValue(GameObject go, float valueFrom, float valueTo, float duration, 
        string strUpdateCallback,
		iTween.EaseType easeType = iTween.EaseType.linear)
	{
		Hashtable htb = new Hashtable ();
        htb.Add ("from", valueFrom);
        htb.Add ("to", valueTo);
		htb.Add ("time", duration);
		htb.Add ("islocal", true);
		htb.Add ("ignoretimescale", true);
		htb.Add ("easetype", easeType);

//        System.Action<float> InnerMethod = (float arg) => 
//            {
//                Debug.Log("-- kekekeke:" + arg);
//            } ;
//		//
//        InnerMethod(11);
        //
        htb.Add ("onupdate", strUpdateCallback);
        //
        iTween.ValueTo(go,htb);
	}
}
