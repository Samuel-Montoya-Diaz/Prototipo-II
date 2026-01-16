using UnityEngine;
using UnityEngine.UI;

using Whisper;
using Whisper.Utils;

public class SimpleMicrophoneWhisper : MonoBehaviour
{
    [Header("Whisper")]
    public WhisperManager whisper;
    public MicrophoneRecord microphone;

    [Header("UI")]
    public Button recordButton;
    public Text buttonText;
    public Text outputText;

    public GeminiChatService chatService;

    private void Awake()
    {
        recordButton.onClick.AddListener(OnButtonPressed);
        microphone.OnRecordStop += OnRecordStop;
    }

    private void OnButtonPressed()
    {
        if (!microphone.IsRecording)
        {
            outputText.text = "Recording...";
            microphone.StartRecord();
            buttonText.text = "Stop";
        }
        else
        {
            microphone.StopRecord();
            buttonText.text = "Record";
        }
    }

    private async void OnRecordStop(AudioChunk audio)
    {
        outputText.text = "Transcribing...";

        var result = await whisper.GetTextAsync(
            audio.Data,
            audio.Frequency,
            audio.Channels
        );

        if (result == null)
        {
            outputText.text = "Error transcribing audio.";
            return;
        }

        outputText.text = result.Result;

        chatService.SendMessage("(Context: you are a criminal talking with Samu). " + outputText.text,
            response => outputText.text += ("\nResponse: " + response),
            error => Debug.LogError(error)
        );
    }
}
