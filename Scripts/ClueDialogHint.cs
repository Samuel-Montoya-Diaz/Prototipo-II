using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClueDialogHint : MonoBehaviour
{
    public string hint;

    public void DisplayHint(HoverEnterEventArgs informationSelect)
    {
        DialogueUIManager.Instance.DisplayAIResponse(hint);
    }
}
