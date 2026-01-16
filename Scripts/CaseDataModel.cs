using System;
using System.Collections.Generic;

[Serializable]
public class ClueState
{
    public string id;
    public bool found;
    public bool inspected_in_vr;
    public string linked_npc_id;
    public string description;
}

[Serializable]
public class NPCState
{
    public int suspicion_level;
    public bool is_interrogated;
    public string current_mood;
    public bool knows_about_culprit;
    public int dialogue_index;
}

[Serializable]
public class NPCStatesContainer
{
    public NPCState NPC_MARCUS;
    public NPCState NPC_ELENA;
    public NPCState NPC_LEO;
}

[Serializable]
public class NarrativeMilestones
{
    public bool leo_confessed_inhibitor;
    public bool marcus_admits_firing;
    public bool elena_threatens_player;
}

[Serializable]
public class DialogueResponse
{
    public string npc_id;
    public string dialogue;
    public int suspicion_change;
}

[Serializable]
public class GameStateRoot
{
    public string case_context;
    public List<ClueState> clues_state;
    public NPCStatesContainer npc_states;
    public NarrativeMilestones narrative_milestones;
    public List<DialogueResponse> previous_dialogs;
}

[Serializable]
public class GeminiNarrativeResult
{
    public DialogueResponse dialogue;
    public NarrativeMilestones narrative_milestones;
}
