using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private BoxCollider doorcollider;
    public Animator animator;
    void Start()
    {
        if(!(TryGetComponent<BoxCollider>(out doorcollider)))
        {
            Debug.Log("attatch collider");
        }
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {   
            animator.Play("door_2_open");
            Debug.Log("Entered");
        }
    }

    public void OnTriggerExit(Collider collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            animator.Play("door_2_close");
        }
    }





}
