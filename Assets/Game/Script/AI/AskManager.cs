using Newtonsoft.Json.Linq;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AskManager : MonoBehaviour
{
    [SerializeField] private GeminiAPI _geminiApi;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button _buttonTest;
    [SerializeField] private Button _buttonGet;

    private void Start()
    {
        _buttonTest.onClick.AddListener(OnClickButtonTest);
        _buttonGet.onClick.AddListener(OnClickButtonAsk);

        _geminiApi.OnReponse?.AddListener(HandleReponse);
    }

    private void OnClickButtonAsk()
    {
        _geminiApi.AskGemini(_inputField.text);
    }

    private void HandleReponse(string arg0)
    {
        SetTextFromGeminiResponse(arg0);
    }

    private void OnClickButtonTest()
    {

    }



    public void SetTextFromGeminiResponse(string json)
    {
        try
        {
            JObject response = JObject.Parse(json);

            string text = (string)response["candidates"]?[0]?["content"]?["parts"]?[0]?["text"];

            if (!string.IsNullOrEmpty(text))
            {
                // Loại bỏ dấu xuống dòng nếu có
                nameInputField.text = text;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy text trong response!");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Lỗi phân tích JSON: " + e.Message);
        }
    }

}
