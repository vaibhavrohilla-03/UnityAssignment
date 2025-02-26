using System;
using System.Collections;
using UnityEngine;

public class QuizManager : MonoBehaviour {
    public static QuizManager instance;

    public GameObject quizPrefab;
    public Vector3 spawnPoint;
    public Vector3 stoppingPoint;
    
    public QuestionSO[] questionSos;
    private int currentQuestionIndex = 0;
    private GameObject currentQuestionGO = null;
    
    private void Start() {
        instance = this;
        NextQuestion();
    }

    public void NextQuestion() {
        if (currentQuestionGO != null) {
            currentQuestionGO.GetComponent<QuizMovement>().StartMoving(Vector3.zero);
        }
        if (currentQuestionIndex < questionSos.Length) {
            SpawnQuestion(questionSos[currentQuestionIndex]);
            currentQuestionIndex++;
        }
        else {
            
        }
    }
    

    public void SpawnQuestion(QuestionSO questionSo) {
        GameObject currentQuestion = Instantiate(quizPrefab, spawnPoint, Quaternion.identity);
        currentQuestionGO = currentQuestion;
        QuizTextSetup quizTextSetup = currentQuestion.GetComponent<QuizTextSetup>();
        quizTextSetup.questionSo = questionSo;
        quizTextSetup.SetText();
        currentQuestion.GetComponent<QuizMovement>().StartMoving(stoppingPoint);
    }

}
