using UnityEngine;

public class NarrativeTest : MonoBehaviour
{
    public GeminiNarrativeDirector director;
    private GameStateRoot gameState = GameStateFactory.CreateInitialState();

    void Start()
    {
        gameState.previous_dialogs.Add(new DialogueResponse() { npc_id = "PLAYER", dialogue = "Hi Elena, who was the first person entering in the room?", suspicion_change = 0 });
        director.GenerateDialogue(
            gameState,
            (dialogue, milestones) =>
            {
                Debug.Log(dialogue.npc_id + ": " + dialogue.dialogue);

                // Aplicar cambios
                gameState.previous_dialogs.Add(dialogue);
                gameState.narrative_milestones = milestones;
            },
            error => Debug.LogError(error)
        );
    }
}
