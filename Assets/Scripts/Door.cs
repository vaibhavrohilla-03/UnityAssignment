using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    
    public Animator animator;

    public void OnTriggerEnter(Collider other)
    {   

        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Drone"))
        {   
            animator.Play("door_2_open");
            Debug.Log("Entered");
        }
    }

    public void OnTriggerExit(Collider other)
    {

        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Drone"))
        {
            animator.Play("door_2_close");
        }
    }





}
