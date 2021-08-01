using UnityEngine;
using System.Collections;

public class iTweenHelper 
{
    public static void MoveGameObject3(GameObject go, Vector3 to, float duration,
        iTween.EaseType easeType = iTween.EaseType.linear, string completedCb = "")
    {
        Vector3 posNew = new Vector3(to.x, to.y, to.z);
        //
        Hashtable htb = new Hashtable();
        htb.Add("position", posNew);
        htb.Add("time", duration);
        htb.Add("islocal", true);
        htb.Add("ignoretimescale", false);
        if (!string.IsNullOrEmpty(completedCb))
        {
            htb.Add("oncomplete", completedCb);
        }
        htb.Add("easetype", easeType);
        //
        iTween.MoveTo(go, htb);
    }


    public static void MoveGameObject(GameObject go, Vector2 to, float duration, 
		iTween.EaseType easeType = iTween.EaseType.linear, string completedCb = "")
	{
		Vector3 posNew = new Vector3(to.x, to.y, 0);
		//
		Hashtable htb = new Hashtable ();
		htb.Add ("position", posNew);
		htb.Add ("time", duration);
		htb.Add ("islocal", true);
		htb.Add ("ignoretimescale", false);
        if (!string.IsNullOrEmpty(completedCb))
        {
            htb.Add ("oncomplete", completedCb);
        }
		htb.Add ("easetype", easeType);
		//
		iTween.MoveTo(go,htb);
	}

    public static void MoveGameObject(GameObject go, Vector2 to, float duration)
    {
        MoveGameObject(go, to, duration, iTween.EaseType.linear);
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
		htb.Add ("ignoretimescale", false);
		htb.Add ("easetype", easeType);
		//
		iTween.ScaleTo(go,htb);
	}


    public static void MakeScaleX(GameObject go, float scaleFrom, float scaleTo, float duration,
        iTween.EaseType easeType = iTween.EaseType.linear)
    {
        go.transform.localScale = new Vector3(scaleFrom, 1f, 1f);
        //
        Hashtable htb = new Hashtable();
        htb.Add("scale", new Vector3(scaleTo, 1f, 1f));
        htb.Add("time", duration);
        htb.Add("islocal", true);
        htb.Add("ignoretimescale", false);
        htb.Add("easetype", easeType);
        //
        iTween.ScaleTo(go, htb);
    }

    public static void MakeScaleY(GameObject go, float scaleFrom, float scaleTo, float duration,
       iTween.EaseType easeType = iTween.EaseType.linear)
    {
        go.transform.localScale = new Vector3(1f, scaleFrom, 1f);
        //
        Hashtable htb = new Hashtable();
        htb.Add("scale", new Vector3(1f, scaleTo, 1f));
        htb.Add("time", duration);
        htb.Add("islocal", true);
        htb.Add("ignoretimescale", false);
        htb.Add("easetype", easeType);
        //
        iTween.ScaleTo(go, htb);
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
		htb.Add ("ignoretimescale", false);
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
