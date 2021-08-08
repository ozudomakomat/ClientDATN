using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    [SerializeField] private bool m_ActiveX=true;
    [SerializeField] private bool m_ActiveY=true;

    void Start()
    {
        SpriteRenderer sr  = GetComponent<SpriteRenderer>();
        Vector3 temp = transform.localScale;

        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        float temp_y = worldHeight / height;
        float temp_x = worldWidth / width;

        if (m_ActiveX) temp = new Vector3( temp_x,temp_x,temp_x); else temp.x = 1;
        if (m_ActiveY) temp = new Vector3(temp_y,temp_y,temp_y); else temp.y = 1;
        transform.localScale = temp;
    }
}
