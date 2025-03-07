using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractableButton : MonoBehaviour
{
    
    private Button button;
    private TextMeshProUGUI textComp;
    [HideInInspector] public Color initialcolor;
    public Color changecolor;
    private void Awake()
    {
        button = GetComponent<Button>();
        textComp = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        initialcolor = textComp.color; 
    }
    
    public void Interact()
    {
        Debug.Log("Interact is called");
        if (button != null)
        {
            button.onClick.Invoke();
        }
        else
        {
            Debug.Log("no button found");
        }
    }

    public void changetextcolor(Color thiscolor)
    {
        textComp.color = thiscolor;
    }


}
