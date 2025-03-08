using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private TypingEffect typer;

    private bool speaking;
    public string dialogue;
    public GameObject CanvasPivot;


    private void Start()
    {
        if(TryGetComponent<TypingEffect>(out typer))
        {
            Debug.Log("typer hai");
        }
        speaking = false;
    }


    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag("Player") && !speaking)
        {   
                StopAllCoroutines();
                typer.ChangeText("");
                StartCoroutine(speak());
                speaking = true;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            stopspeaking();
            speaking = false;

        }
    }

    private IEnumerator speak()
    {
        yield return new WaitForSeconds(1.0f);
        LeanTween.scale(CanvasPivot.gameObject, Vector3.one, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1.5f);
        typer.StartTyping(dialogue,20.0f);
        yield return null;
    }

    private void stopspeaking()
    {
        LeanTween.scale(CanvasPivot.gameObject, Vector3.zero, 1.0f).setEase(LeanTweenType.easeOutQuad);
        typer.ChangeText("");
    }
}
