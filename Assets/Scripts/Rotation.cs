using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 0.5f));        
    }
}
