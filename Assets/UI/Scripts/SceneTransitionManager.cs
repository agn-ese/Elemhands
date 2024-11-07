using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreenUI fadeScreenUI;
    [Tooltip("Useful for videos that need to trigger a scene change when they finish playing.")]
    public VideoPlayer videoPlayer;

    void Start()
    {
        if(videoPlayer != null) videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        GoToScene(1);
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreenUI.FadeOut();
        yield return new WaitForSeconds(fadeScreenUI.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
    }

}
