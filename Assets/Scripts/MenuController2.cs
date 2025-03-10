using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController2 : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject Background;
    

    private bool opened = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (opened)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }



    private void OpenMenu()
    {
        opened = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuCanvas.SetActive(true);
        Background.SetActive(true);
        

    }

    private void CloseMenu()
    {
        opened = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        menuCanvas.SetActive(false);
        Background.SetActive(false);
      
    }

}