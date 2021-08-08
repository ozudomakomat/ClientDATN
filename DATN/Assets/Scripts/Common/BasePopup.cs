using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasePopup : MonoBehaviour
{
    public GameObject m_GoContent;
    protected virtual void Start()
    {
        PopContentUp();
    }
    private void PopContentUp()
    {
        if (m_GoContent != null)
        {
            iTweenHelper.MakeScale(m_GoContent, 0.7f, 1f, 0.1f);
        }
    }
    public void ClosePopup()
    {
        Vector2 to = new Vector2(transform.position.x, transform.position.y + SaveData.heigthCanvas);
        iTweenHelper.MakeScale(m_GoContent, 1f, 0.7f, 0.1f);
        Destroy(gameObject, 0.1f);
    }
    protected void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    protected void LoadScene(string SceneName, bool playMusicScene = false, float volumn = 1f)
    {
        SceneManager.LoadScene(SceneName);
        if (playMusicScene)
        {
            AudioClip sceneMusic = Utils.LoadAudioClip(SceneName);
            SoundManager.Instance.PlayMusicClip(sceneMusic, volumn);
        }
    }
}
