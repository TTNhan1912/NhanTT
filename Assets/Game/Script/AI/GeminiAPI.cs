using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GeminiAPI : MonoBehaviour
{
    public UnityEvent<string> OnReponse;

    private const string API_KEY = "AIzaSyDiO4npTFDA7WcScEx80FB_hdIgTQD0DjU"; // Thay bằng API Key của bạn
    private const string URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + API_KEY;

    public void AskGemini(string userInput)
    {
        StartCoroutine(SendRequest(userInput));
    }

    private IEnumerator SendRequest(string prompt)
    {
        // Chuẩn bị nội dung JSON để gửi lên API
        string jsonData = "{\"contents\": [{\"parts\": [{\"text\": \"" + prompt + "\"}]}]}";
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(URL, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                OnReponse?.Invoke(response);
                Debug.Log("Gemini Response: " + response);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}
