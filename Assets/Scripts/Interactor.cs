using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource; 
    public float InteractRange = 3f;
    LayerMask mask;
    private InteractableButton Ibutton;
    private InteractableButton lastIbutton;

    private void Start()
    {
        mask = LayerMask.GetMask("Interactable");
    }
    private void Update()
    {
            if (Physics.Raycast(InteractorSource.position, InteractorSource.forward, out RaycastHit hitInfo, InteractRange,mask))
            {   
               
                if (hitInfo.collider.gameObject.TryGetComponent(out  Ibutton))
                {
                     if (lastIbutton && (lastIbutton != Ibutton))
                        {
                            lastIbutton.changetextcolor(lastIbutton.initialcolor);
                        }

                    Ibutton.changetextcolor(Ibutton.changecolor);
                    lastIbutton = Ibutton;
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("pressed");
                        Ibutton.Interact();
                    }
                }
            }
            else
            {
                if(Ibutton != null)
                {
                    Ibutton.changetextcolor(Ibutton.initialcolor);
                }
            }
        
    }
}
