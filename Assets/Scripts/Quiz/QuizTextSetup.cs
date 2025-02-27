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
        if (index == questionSo.correctAnswerIndex) {
            QuizManager.instance.IncreaseScore();
            QuizManager.instance.SetScore();
        }
        StartCoroutine(ShowQuestionResult(index == questionSo.correctAnswerIndex, index));
    }
    
    IEnumerator ShowQuestionResult(bool result, int index) {
        optionA.interactable = false;
        optionB.interactable = false;
        optionC.interactable = false;
        optionD.interactable = false;
        questionText.text = ((result) ? "Correct !!" : "Incorrect") + 
                            (string.Compare(questionSo.wrongSelection[index], "Null") == 0 ? "" : "\n" + questionSo.wrongSelection[index]);
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
