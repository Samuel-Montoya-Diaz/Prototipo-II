using UnityEngine;
using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

public class GeminiChatService : MonoBehaviour
{
    [Header("Config")]
    public string apiKey;
    public string model = "gemini-2.5-flash";
    private string baseUrl = "https://generativelanguage.googleapis.com/v1beta/";

    /// <summary>
    /// Envía un mensaje a Gemini y devuelve la respuesta por callback.
    /// </summary>
    public void SendMessage(string prompt, Action<string> onResponse, Action<string> onError)
    {
        CoroutineRunner.Instance.Run(SendRequestCoroutine(prompt, onResponse, onError));
    }

    private IEnumerator SendRequestCoroutine(
        string prompt,
        Action<string> onResponse,
        Action<string> onError
    )
    {
        string url = $"{baseUrl}models/{model}:generateContent";

        // Construimos el JSON según la API de Gemini
        string json = BuildJson(prompt);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Headers: content-type + llave
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("x-goog-api-key", apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            string parsed = GeminiResponseCleaner.ExtractDialogueJson(responseText);
            onResponse?.Invoke(parsed);
        }
    }

    private string BuildJson(string prompt)
    {
        // Gemini espera este formato: "contents": [ { "parts": [ { "text": ... } ] } ]
        return
            "{\n" +
            "  \"contents\": [\n" +
            "    {\n" +
            "      \"parts\": [\n" +
            "        { \"text\": \"" + Escape(prompt) + "\" }\n" +
            "      ]\n" +
            "    }\n" +
            "  ]\n" +
            "}";
    }

    private string Escape(string s)
    {
        return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}
