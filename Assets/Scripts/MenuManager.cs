using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject canvas;
    [SerializeField] AudioSettings audioSettings;
    
    private void Start()
    {
        animator.Play("Fadein");
        if(audioSettings != null)
        {
            audioSettings.LoadAudioSettings();
        }
    }
    public void MoveToGamePlay()
    {
        Debug.Log("Pressed");
        animator.Play("FadeEnd");
        var coroutine = ChangeScene(2, 3);
        StartCoroutine(coroutine);
    }

    public void MoveToMainMenu()
    {
        Debug.Log("Pressed");
        animator.Play("PressStartMenu");
        var coroutine = ChangeScene(1, 3);
        StartCoroutine(coroutine);
    }

    IEnumerator ChangeScene(int sceneIndex, int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
