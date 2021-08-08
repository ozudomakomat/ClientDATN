using UnityEngine;
using UnityEngine.UI;

public class ReScaleCanvas : MonoBehaviour
{
    [SerializeField] float w = 1080;
    [SerializeField] float h = 1920;
    private float ratioMin = 0.45f; // w/h
    private float ratioMax = 0.75f; // w/h
    void Start() {
        ReScale();
    }

    private void ReScale() {

        float ratioMed = w / h; 
        float scale = 1;

#if UNITY_EDITOR
        Vector2 size = UnityEditor.Handles.GetMainGameViewSize();
#else
        Vector2 size = new Vector2(Screen.width, Screen.height);
#endif
        float r = size.x / size.y;
        //if (r >= size.x / size.y) {
        //    scale = size.x / w;
        //} else {
        //    scale = size.y / h;
        //}
        //GetComponent<CanvasScaler>().matchWidthOrHeight = scale;

        scale = Mathf.Clamp((r - ratioMin) / (ratioMax - ratioMin), 0 , 1);

        GetComponent<CanvasScaler>().matchWidthOrHeight = scale;
    }
}
