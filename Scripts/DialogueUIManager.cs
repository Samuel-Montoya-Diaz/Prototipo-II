using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance;

    [Header("Configuración de UI")]
    public TextMeshPro dialogueText;
    public float typingSpeed = 0.04f;

    private Coroutine typingCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// Método principal para mostrar texto desde cualquier script.
    /// <param name="message">El texto de la IA</param>
    /// <param name="autoHideTime">Si es mayor a 0, se oculta solo tras X segundos</param>
    public void DisplayAIResponse(string message, float autoHideTime = 5f)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(message, autoHideTime));
    }

    private IEnumerator TypeText(string message, float delay)
    {
        dialogueText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
            HideDialogue();
        }
    }

    public void HideDialogue()
    {
        dialogueText.text = "";
    }
}