using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class QuizManager : MonoBehaviour {
    public static QuizManager instance;

    public GameObject quizPrefab;
    public Vector3 spawnPoint;
    public Vector3 stoppingPoint;

    public QuestionSO[] questionSos;
    private int currentQuestionIndex = 0;
    private GameObject currentQuestionGO = null;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI feedbackText; // Added for explanations

    public int score = 0;
    public float timeLimit = 10f; // Time per question
    private float currentTime;
    private bool isTimerRunning = false;
    private bool answered = false;

    private string geminiApiKey = "AIzaSyAMjcz4ufjBuKAGiyZC8uuOvh-wKM9JcQ8"; // Replace with your actual API key
    private string geminiModel = "gemini-1.5-flash";

    private void Start() {
        instance = this;
        NextQuestion();
        Debug.Log("Hey there");
    }

    private void Update() {
        if (isTimerRunning && !answered) {
            currentTime -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(currentTime) + "s";

            if (currentTime <= 0) {
                feedbackText.text = "Time's up!";
                answered = true;
                Invoke(nameof(NextQuestion), 2f);
            }
        }
    }

    public void NextQuestion() {
        if (currentQuestionGO != null) {
            currentQuestionGO.GetComponent<QuizMovement>().StartMoving(Vector3.zero);
        }

        if (currentQuestionIndex < questionSos.Length) {
            SpawnQuestion(questionSos[currentQuestionIndex]);
            currentQuestionIndex++;
            ResetTimer();
        } else {
            feedbackText.text = "Quiz Complete!";
            Invoke("ChangeScene", 5f);
        }
    }

    private void ChangeScene() {
        SceneManager.LoadScene(1);
    }

    public void SpawnQuestion(QuestionSO questionSo) {
        answered = false;
        UpdateFeedbackText(""); // Clear previous feedback

        GameObject currentQuestion = Instantiate(quizPrefab, spawnPoint, Quaternion.identity);
        currentQuestionGO = currentQuestion;
        QuizTextSetup quizTextSetup = currentQuestion.GetComponent<QuizTextSetup>();
        quizTextSetup.questionSo = questionSo;
        quizTextSetup.SetText();
        currentQuestion.GetComponent<QuizMovement>().StartMoving(stoppingPoint);
    }

    public void IncreaseScore() {
        score++;
        SetScore();
    }

    public void SetScore() {
        scoreText.text = "Score\n" + score + " / " + questionSos.Length;
    }

    private void ResetTimer() {
        currentTime = timeLimit;
        isTimerRunning = true;
        answered = false;
    }

    public void CheckAnswer(int selectedIndex) {
        if (answered) return;
        answered = true;
        Debug.Log("CheckAnswer triggered with index: " + selectedIndex);

        QuestionSO q = questionSos[currentQuestionIndex - 1];

        if (selectedIndex == q.correctAnswerIndex) {
            UpdateFeedbackText("Correct!");
            IncreaseScore();
            Invoke(nameof(NextQuestion), 1.5f);
        } else {
            UpdateFeedbackText("Incorrect! Getting explanation...");
            StartCoroutine(GetGeminiExplanation(q.question, q.answers[selectedIndex], q.answers[q.correctAnswerIndex]));
        }
    }

    IEnumerator GetGeminiExplanation(string question, string wrongAnswer, string correctAnswer) {
        string prompt = $"Question: {question}\nUser's Wrong Answer: {wrongAnswer}\nCorrect Answer: {correctAnswer}\nExplain why the wrong answer is incorrect and provide the correct answer in just 1-2 lines.";

        var requestBody = new {
            contents = new[] {
                new {
                    role = "user",
                    parts = new[] { new { text = prompt } }
                }
            }
        };

        string jsonBody = JsonConvert.SerializeObject(requestBody);
        string apiUrl = $"https://generativelanguage.googleapis.com/v1/models/{geminiModel}:generateContent?key={geminiApiKey}";

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST")) {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("Sending API request...");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                string responseText = request.downloadHandler.text;
                Debug.Log("Response from Gemini API: " + responseText); // Debug API response

                GeminiResponse response = JsonConvert.DeserializeObject<GeminiResponse>(responseText);
                if (response != null && response.candidates.Length > 0) {
                    string explanation = response.candidates[0].content.parts[0].text;
                    Debug.Log("Explanation: " + explanation); // Debug parsed text
                    UpdateFeedbackText(explanation);
                } else {
                    UpdateFeedbackText("Unexpected API response!");
                    Debug.LogWarning("Unexpected API response structure");
                }
            } else {
                UpdateFeedbackText("Error getting explanation!");
                Debug.LogError("API Request Failed: " + request.error);
            }

            Invoke(nameof(NextQuestion), 3f);
        }
    }

    private void UpdateFeedbackText(string text) {
        feedbackText.gameObject.SetActive(false); // Force UI refresh
        feedbackText.text = text;
        feedbackText.gameObject.SetActive(true);
    }

    [System.Serializable]
    public class GeminiResponse {
        public Candidate[] candidates;
    }

    [System.Serializable]
    public class Candidate {
        public Content content;
    }

    [System.Serializable]
    public class Content {
        public Part[] parts;
    }

    [System.Serializable]
    public class Part {
        public string text;
    }
}
