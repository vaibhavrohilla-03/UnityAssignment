using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScene : MonoBehaviour
{  
    

    public void changescene(string SceneName)
    {
        if(SceneName =="MainMenu")
        {
            Cursor.visible = true;
            SceneManager.LoadScene(SceneName);
        }
        if(SceneName == "GameMode" || SceneName == "TutorialScene")
        {
            Cursor.visible = false;
            SceneManager.LoadScene(SceneName);  
        }

    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}
