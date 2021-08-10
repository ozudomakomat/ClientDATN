using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSteel : MonoBehaviour
{

    [System.Obsolete]
    private void OnEnable()
    {
        StartCoroutine(showItem());
    }


    [System.Obsolete]
    private void OnDisable()
    {
        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    [System.Obsolete]
    IEnumerator showItem()
    {
        for (int i = 0; i < gameObject.transform.GetChildCount(); i++)
        {
            yield return new WaitForSeconds(0.05f);
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
