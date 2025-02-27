using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMovement : MonoBehaviour {
    
    public void StartMoving(Vector3 target) {
        StartCoroutine(StartMovingCou(target));
    }

    IEnumerator StartMovingCou(Vector3 target) {
        while (transform.position.z > target.z) {
            transform.Translate(0f, 0f, -2.5f * Time.deltaTime);
            yield return null;
        }
        GetComponent<QuizTextSetup>().EnableOptions();
        if (transform.position.z <= 0f) {
            Destroy(gameObject);
        }
    }
}
