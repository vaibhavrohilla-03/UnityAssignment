using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizTextSetup : MonoBehaviour {
    
    public TextMeshProUGUI questionText;
    public Button optionA;
    public Button optionB;
    public Button optionC;
    public Button optionD;
    
    public QuestionSO questionSo;

    public void SetText() {
        questionText.text = questionSo.question + "\n" +
                            questionSo.answers[0] + "\n" +
                            questionSo.answers[1] + "\n" +
                            questionSo.answers[2] + "\n" +
                            questionSo.answers[3] + "\n";
    }

    public void CheckAnswer(int index) {
        StartCoroutine(ShowQuestionResult(index == questionSo.correctAnswerIndex));
    }
    
    IEnumerator ShowQuestionResult(bool result) {
        optionA.interactable = false;
        optionB.interactable = false;
        optionC.interactable = false;
        optionD.interactable = false;
        questionText.text = (result) ? "Correct !!" : "Incorrect";
        yield return new WaitForSeconds(4f);
        QuizManager.instance.NextQuestion();
    }

    public void EnableOptions() {
        optionA.interactable = true;
        optionB.interactable = true;
        optionC.interactable = true;
        optionD.interactable = true;
    }
}
