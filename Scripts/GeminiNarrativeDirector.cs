using System;
using System.Text;
using UnityEngine;

public class GeminiNarrativeDirector : MonoBehaviour
{
    public GeminiChatService gemini;

    /// <summary>
    /// Genera un diálogo narrativo basado en el estado del juego
    /// </summary>
    public void GenerateDialogue(
        GameStateRoot gameState,
        Action<DialogueResponse, NarrativeMilestones> onResult,
        Action<string> onError
    )
    {
        string prompt = BuildPrompt(gameState);
        Debug.Log( prompt );

        gemini.SendMessage(
            prompt,
            responseJson =>
            {
                try
                {
                    Debug.Log(responseJson);
                    GeminiNarrativeResult result =
                        JsonUtility.FromJson<GeminiNarrativeResult>(responseJson);

                    if (result == null || result.dialogue == null)
                    {
                        onError?.Invoke("Invalid Gemini response");
                        return;
                    }

                    onResult?.Invoke(
                        result.dialogue,
                        result.narrative_milestones
                    );
                }
                catch (Exception e)
                {
                    onError?.Invoke("JSON Parse Error: " + e.Message);
                }
            },
            onError
        );
    }

    // ---------------- PROMPT ----------------

    private string BuildPrompt(GameStateRoot state)
    {
        StringBuilder sb = new StringBuilder();

        // ----- CONTEXTO GENERAL -----
        sb.AppendLine("You are the narrative director of a visual novel.\n");
        sb.AppendLine("CASE CONTEXT:");
        sb.AppendLine(state.case_context);
        sb.AppendLine();

        // ----- NPCs -----
        sb.AppendLine("NPCs states:");
        sb.AppendLine($"Marcus: suspicion={state.npc_states.NPC_MARCUS.suspicion_level}, mood={state.npc_states.NPC_MARCUS.current_mood}");
        sb.AppendLine($"Elena: suspicion={state.npc_states.NPC_ELENA.suspicion_level}, mood={state.npc_states.NPC_ELENA.current_mood}");
        sb.AppendLine($"Leo: suspicion={state.npc_states.NPC_LEO.suspicion_level}, mood={state.npc_states.NPC_LEO.current_mood}");
        sb.AppendLine();

        // ----- PISTAS -----
        sb.AppendLine("FOUNDED CLUES:");
        foreach (var clue in state.clues_state)
        {
            if (clue.found)
                sb.AppendLine($"- {clue.description}");
        }
        sb.AppendLine();

        // ----- HITOS NARRATIVOS -----
        sb.AppendLine("NARRATIVE MILESTONES:");
        sb.AppendLine($"Leo confessed inhibitor: {state.narrative_milestones.leo_confessed_inhibitor}");
        sb.AppendLine($"Marcus admits firing: {state.narrative_milestones.marcus_admits_firing}");
        sb.AppendLine($"Elena threatens player: {state.narrative_milestones.elena_threatens_player}");
        sb.AppendLine();

        // ----- HISTORIAL DE DIÁLOGOS -----
        sb.AppendLine("PREVIOUS DIALOGS:");
        foreach (var d in state.previous_dialogs)
        {
            sb.AppendLine($"{d.npc_id}: {d.dialogue} (suspicion_change={d.suspicion_change})");
        }
        sb.AppendLine();

        // ----- INSTRUCCIONES PARA GEMINI -----
        sb.AppendLine("TASK:");
        sb.AppendLine(
            @"
Choose ONE NPC to speak next and generate their dialogue.

You may also update narrative milestones ONLY if the dialogue clearly reveals them.

RULES:
- Respond ONLY with valid JSON
- Do NOT add explanations
- Do NOT use markdown
- Do NOT include text outside JSON
- Use EXACTLY this schema:

{
  ""dialogue"": {
    ""npc_id"": ""NPC_MARCUS | NPC_ELENA | NPC_LEO"",
    ""dialogue"": ""string"",
    ""suspicion_change"": int
  },
  ""narrative_milestones"": {
    ""leo_confessed_inhibitor"": bool,
    ""marcus_admits_firing"": bool,
    ""elena_threatens_player"": bool
  }
}

CONSTRAINTS:
- suspicion_change must be between -20 and +20
- suspicion_change should reflect the dialogue tone
- narrative milestones must NEVER revert from true to false
- choose an NPC that makes narrative sense
        ");

        return sb.ToString();
    }
}
