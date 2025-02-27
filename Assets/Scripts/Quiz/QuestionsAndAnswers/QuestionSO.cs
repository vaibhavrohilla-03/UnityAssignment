using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question")]

public class QuestionSO : ScriptableObject {
    [TextArea(2, 5)]
    public string question;  // The quiz question

    [SerializeField]
    public string[] answers; // Array of possible answers

    [Range(0, 3)]
    public int correctAnswerIndex; // Index of the correct answer in the array
    
    [SerializeField]
    public string[] wrongSelection;
}
