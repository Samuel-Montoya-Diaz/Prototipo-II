using UnityEngine;
using Whisper;
using Whisper.Utils;
using static UnityEngine.InputSystem.InputAction;

public class GameStateManager : MonoBehaviour
{
    public WhisperManager whisper;
    public MicrophoneRecord microphone;
    public GeminiNarrativeDirector gemini;
    public DialogueUIManager dialogue;
    private GameStateRoot gameState = GameStateFactory.CreateInitialState();

    public void Awake()
    {
        microphone.OnRecordStop += OnRecordStop;
    }
    public void OnRecording(CallbackContext ctx)
    {
        if (!microphone.IsRecording)
        {
            dialogue.DisplayAIResponse("Recording...");
            microphone.StartRecord();
        }
        else
        {
            microphone.StopRecord();
        }
    }

    public async void OnRecordStop(AudioChunk audio)
    {
        var result = await whisper.GetTextAsync(
            audio.Data,
            audio.Frequency,
            audio.Channels
        );

        if (result == null)
        {
            Debug.LogError("Error transcribing audio.");
            return;
        }

        gameState.previous_dialogs.Add(new DialogueResponse { npc_id = "Player", dialogue = result.Result, suspicion_change = 0 });
        dialogue.DisplayAIResponse(result.Result, 0.5f);
        Debug.Log(result.Result);

        gemini.GenerateDialogue(gameState, (dialog, milestones) => {
            gameState.previous_dialogs.Add(dialog);
            dialogue.DisplayAIResponse(dialog.npc_id + ": " + dialog.dialogue);
        }, error => Debug.LogError(error));
    }
}
