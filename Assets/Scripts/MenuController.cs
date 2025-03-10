using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject Background;
    public GameObject Crosshair;
    public GameObject thirdPersonController;

    private bool opened = false;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if(opened)
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
        Crosshair.SetActive(false);
        thirdPersonController.SetActive(false);

    }

    private void CloseMenu()
    {   
        opened = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        menuCanvas.SetActive(false);
        Background.SetActive(false);
        Crosshair.SetActive(true);
        thirdPersonController.SetActive(true);
    }

}
