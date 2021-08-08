using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{

    private Vector2 firstHero;
    private Camera camera;
    [SerializeField] GameObject target;
    private float WidthScene
    {
        get
        {
            return Game.Instance.gameLoader.WidthScene;
        }
    }

    private void Awake()
    {
        if (camera == null) camera = GetComponent<Camera>();
        EventDispatcher.Instance.AddListener(EventName.SHAKE_CAMERA_UP_DOWN, ShakeCamera);
        EventDispatcher.Instance.AddListener(EventName.SHAKE_CAMERA_UP, ShakeCameraUp);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventName.SHAKE_CAMERA_UP_DOWN, ShakeCamera);
        EventDispatcher.Instance.RemoveListener(EventName.SHAKE_CAMERA_UP, ShakeCameraUp);
    }

    private void ShakeCamera(EventName key, object data)
    {
        float Decay = (float)data, length = 0.1f;
        float posY = camera.transform.position.y;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(camera.transform.DOMoveY(posY - Decay, length)).Append(camera.transform.DOMoveY(posY + Decay * 0.8f, length))
            .Append(camera.transform.DOMoveY(posY - Decay * 0.7f, length)).Append(camera.transform.DOMoveY(posY + Decay * 0.5f, length))
            .Append(camera.transform.DOMoveY(posY - Decay * 0.3f, length)).Append(camera.transform.DOMoveY(posY, length));
    }

    private void ShakeCameraUp(EventName key, object data)
    {
        float Decay = (float)data, length = 0.1f;
        float posY = camera.transform.position.y;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(camera.transform.DOMoveY(posY - Decay, length)).Append(camera.transform.DOMoveY(posY , length));
    }
}
