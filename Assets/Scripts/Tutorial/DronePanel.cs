using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DronePanel : MonoBehaviour
{
    public TextMeshProUGUI textcomp;

    public void AnimatePopup()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void AnimatePopDown()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEase(LeanTweenType.easeInOutQuad);

    }


   
   
}
