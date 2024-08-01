using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator buttonAnim;

    private string targetScene;
    bool isExiting;
    public void LoadLevel1()
    {
        targetScene = "Level1";
        isExiting = false;
        buttonAnim.SetTrigger("PlaySlideOut");

    }

    public void ExitGame()
    {
        targetScene = null;
        isExiting = true;
        buttonAnim.SetTrigger("ExitSlideOut");
    }

    public void OnAnimationComplete()
    {
        if(!isExiting)
        {
            SceneManager.LoadScene(targetScene);
        }
        else
        {
            Application.Quit();
        }
    }
}
