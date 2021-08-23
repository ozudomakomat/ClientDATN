using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BaseScene :  MonoEventHandler
{
    protected void LoadScene(string SceneName, bool playMusicScene = false, float volumn = 1f)
    {
        SceneManager.LoadScene(SceneName);
        if (playMusicScene) {
            AudioClip sceneMusic = Utils.LoadAudioClip(SceneName);
            SoundManager.Instance.PlayMusicClip(sceneMusic, volumn);
        }
    }
    public override void ProcessKEvent(int eventId, object data)
    {
        base.ProcessKEvent(eventId, data);
    }
}
