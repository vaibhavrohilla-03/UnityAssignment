using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tutorial : MonoBehaviour
{
    public NavMeshAgent drone;
    public GameObject dronepivot;
    public DronePanel dronepanel;
    public TypingEffect Typer;
    void Start()
    {
        
    }

    
    private IEnumerator TutorialRoutine()
    {   

        yield return null;
    }


    private IEnumerator InitiateTutorial()
    {
        LeanTween.scale(dronepivot, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(dronepanel.gameObject, Vector3.one, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1.0f);
        
        Typer.StartTyping("Hello there",20.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(InitiateTutorial());
        }
    }
}
