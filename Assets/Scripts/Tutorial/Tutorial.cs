using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tutorial : MonoBehaviour
{
    public NavMeshAgent drone;
    public Transform TargetOne;
    public Transform TargetTwo;
    public Transform TargetThree;
    public GameObject dronepivot;
    public DronePanel dronepanel;
    public TypingEffect Typer;
    public Transform playertransform;
    private bool running =  false;
    private WaitForSeconds wait;

    private IEnumerator InitiateTutorial()
    {   
        running = true; 
        LeanTween.scale(dronepivot, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(1.5f);
        LeanTween.scale(dronepanel.gameObject, Vector3.one, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(1.0f);
        
        Typer.StartTyping("Hello there!!!",20.0f);
        yield return newwait(2.5f);

        Typer.StartTyping("Welcome to my Space Shuttle", 20.0f);
        yield return newwait(2.5f);

        Typer.StartTyping("I am your Guide for Today, Come Follow me", 20.0f);
        yield return newwait(4.0f);

        LeanTween.scale(dronepanel.gameObject, Vector3.zero, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(1.0f);

        drone.destination = TargetOne.position;
        yield return new WaitUntil(() => !drone.pathPending && drone.remainingDistance <= drone.stoppingDistance);

        Lookat(playertransform.position - drone.transform.position);
        yield return newwait(2.5f);

        LeanTween.scale(dronepanel.gameObject, Vector3.one, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(2.0f);

        Typer.StartTyping("", 20.0f);

        Typer.StartTyping("In this Shuttle there are 3 rooms", 20.0f);
        yield return newwait(3.0f);

        Typer.StartTyping("The middle room is the quiz room", 20.0f);
        yield return newwait(2.5f);

        Typer.StartTyping("And the room behind me is the DataVisualization room", 20.0f);
        yield return newwait(4.5f);

        Typer.StartTyping("and for the last room is where i chill With my friends ", 20.0f);
        yield return newwait(4.5f);

        Typer.StartTyping("for now just follow me feel free to explore later on", 20.0f);
        yield return newwait(4.5f);

        LeanTween.scale(dronepanel.gameObject, Vector3.zero, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(1.0f);

        drone.destination = TargetTwo.position;
        yield return new WaitUntil(() => !drone.pathPending && drone.remainingDistance <= drone.stoppingDistance);

        Lookat(playertransform.position - drone.transform.position);
        yield return newwait(2.5f);


        LeanTween.scale(dronepanel.gameObject, Vector3.one, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(2.0f);

        Typer.StartTyping("You know Data Science is all about extracting insights from data", 20.0f);
        yield return newwait(5.5f);


        Typer.StartTyping("Data Visualization is one of the most important things we do in dataSciene", 20.0f);
        yield return newwait(5.5f);


        Typer.StartTyping("In this shuttle, we use visualization to present data in more understandable way", 20.0f);
        yield return newwait(5.5f);

        Typer.StartTyping("We have some  Example Data for you to visualize", 20.0f);
        yield return newwait(5.5f);

        Typer.StartTyping("Just go to the Console and Press E to interact with the Console", 20.0f);
        yield return newwait(5.5f);

        Typer.StartTyping("For now I am gonna Continue with my work feel free to explore the shuttle and the quiz we made for you", 20.0f);
        yield return newwait(6.5f);

        LeanTween.scale(dronepanel.gameObject, Vector3.zero, 1.0f).setEase(LeanTweenType.easeOutQuad);
        yield return newwait(1.0f);

        drone.destination = TargetThree.position;
        yield return new WaitUntil(() => !drone.pathPending && drone.remainingDistance <= drone.stoppingDistance);


    }

    private WaitForSeconds newwait(float seconds)
    {
        wait = new WaitForSeconds(seconds);
        return wait;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !running)
        {
            StartCoroutine(InitiateTutorial());
        }
    }

    private void Lookat(Vector3 direction)
    {
        direction.y = 0; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        LeanTween.rotate(drone.gameObject, targetRotation.eulerAngles, 1.0f).setEase(LeanTweenType.easeOutQuad);
    }


}
