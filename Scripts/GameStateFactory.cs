using System.Collections.Generic;

public static class GameStateFactory
{
    public static GameStateRoot CreateInitialState()
    {
        return new GameStateRoot
        {
            case_context = @"
The prestigious art dealer and money launderer, Julian Vane, lies dead at his desk in his private office.
There are no signs of a violent struggle, only an overturned glass of brandy and a strange burn on his left wrist that suggests the use of a high-frequency electrical device.
The security door locked automatically after the incident, trapping his three closest guests at the scene.
The player's goal is to recover the missing ‘Account Book’ and identify the killer before the security system resets and the doors open.
The tension lies in the fact that the Account Book is not a physical object, but a digital key recorded on a biometric device.
The killer was unable to take it because it requires Julian's fingerprint, which they tried to extract by force (explaining the burn), but something went wrong and Julian died of induced cardiac arrest.
The culprit still has the device on them, waiting for the moment to dispose of it or force their way out.",

            clues_state = new List<ClueState>
            {
                new ClueState
                {
                    id = "CLUE_CUFFLINK",
                    found = false,
                    inspected_in_vr = false,
                    linked_npc_id = "NPC_MARCUS",
                    description =
                        "A single luxury cufflink found under the carpet near the body. " +
                        "It belongs to an exclusive brand and appears recently dropped."
                },
                new ClueState
                {
                    id = "CLUE_LIPSTICK_GLASS",
                    found = false,
                    inspected_in_vr = false,
                    linked_npc_id = "NPC_ELENA",
                    description =
                        "A barely visible lipstick residue on the rim of a fine crystal glass. " +
                        "It does not belong to Julian."
                },
                new ClueState
                {
                    id = "CLUE_SIGNAL_INHIBITOR",
                    found = false,
                    inspected_in_vr = false,
                    linked_npc_id = "NPC_LEO",
                    description =
                        "A small electronic signal inhibitor hidden behind a bookcase. " +
                        "It blocked the security cameras for two minutes before the murder."
                },
                new ClueState
                {
                    id = "CLUE_BLACKMAIL_NOTE",
                    found = false,
                    inspected_in_vr = false,
                    linked_npc_id = "",
                    description =
                        "A crumpled note found in the trash: " +
                        "'I know about the Swiss account. Pay today, or the Ledger goes public.'"
                },
                new ClueState
                {
                    id = "CLUE_VR_AUDIO",
                    found = false,
                    inspected_in_vr = false,
                    linked_npc_id = "NPC_ELENA",
                    description =
                        "A VR voice recorder. When activated, it plays the last ten seconds of audio: " +
                        "a brief struggle and a female voice whispering, 'Just give me the code, Julian.'"
                }
            },

            npc_states = new NPCStatesContainer
            {
                NPC_MARCUS = new NPCState
                {
                    suspicion_level = 35,
                    is_interrogated = false,
                    current_mood = "nervous",
                    knows_about_culprit = false,
                    dialogue_index = 0
                },
                NPC_ELENA = new NPCState
                {
                    suspicion_level = 15,
                    is_interrogated = false,
                    current_mood = "calm",
                    knows_about_culprit = true,
                    dialogue_index = 0
                },
                NPC_LEO = new NPCState
                {
                    suspicion_level = 45,
                    is_interrogated = false,
                    current_mood = "paranoid",
                    knows_about_culprit = false,
                    dialogue_index = 0
                }
            },

            narrative_milestones = new NarrativeMilestones
            {
                leo_confessed_inhibitor = false,
                marcus_admits_firing = false,
                elena_threatens_player = false
            },

            previous_dialogs = new List<DialogueResponse>()
        };
    }
}
