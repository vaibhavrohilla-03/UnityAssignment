using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public AudioSource Opensource;
    public AudioSource Closesource;

    private bool isOpen = false;

    private void Start()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Drone")) && !isOpen)
        {
            animator.Play("door_2_open");
            Debug.Log("Entered");
            Opensource.Play();
            isOpen = true; 
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Drone")) && isOpen)
        {
            animator.Play("door_2_close");
            Closesource.Play();
            isOpen = false; 
        }
    }
}
