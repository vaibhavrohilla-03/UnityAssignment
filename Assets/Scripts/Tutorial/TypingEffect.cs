using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI Paneltextcomp;
    private  WaitForSeconds simpledelay;
    private int currentvisiblecharacters = 0;

    private void Start()
    {
        ChangeText("");
    }
    
    public void StartTyping(string text,float characterpersec)
    {
        StopAllCoroutines();
        StartCoroutine(TypingRoutine(text, characterpersec));
    }

    public IEnumerator TypingRoutine(string newtext, float speed)
    {   
        ChangeText(newtext);
        TMP_TextInfo info = Paneltextcomp.GetTextInfo(newtext);
        simpledelay = new WaitForSeconds(1.0f / speed);
        while (currentvisiblecharacters < info.characterCount +1)
        {
            Paneltextcomp.maxVisibleCharacters = currentvisiblecharacters;
            yield return simpledelay;
            currentvisiblecharacters++;
        }

        yield return null;
    }

    public void ChangeText(string newtext)
    {
        Paneltextcomp.text = newtext;
        Paneltextcomp.maxVisibleCharacters = 0;
        currentvisiblecharacters = 0;
    }
}
