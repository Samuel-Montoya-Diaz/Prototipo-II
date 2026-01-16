using System;
using UnityEngine;

public static class GeminiResponseCleaner
{
    /// <summary>
    /// Extrae el JSON generado por Gemini de la estructura de respuesta completa
    /// </summary>
    /// <param name="rawResponse">Texto completo de Gemini</param>
    /// <returns>JSON limpio listo para JsonUtility.FromJson</returns>
    public static string ExtractDialogueJson(string rawResponse)
    {
        if (string.IsNullOrEmpty(rawResponse))
            return null;

        try
        {
            // Primero, deserializamos al objeto mínimo para acceder a candidates
            var wrapper = JsonUtility.FromJson<GeminiWrapper>(rawResponse);

            if (wrapper == null || wrapper.candidates == null || wrapper.candidates.Length == 0)
                return null;

            var firstCandidate = wrapper.candidates[0];

            if (firstCandidate == null || firstCandidate.content == null || firstCandidate.content.parts == null || firstCandidate.content.parts.Length == 0)
                return null;

            // El texto que contiene el JSON real
            string text = firstCandidate.content.parts[0].text;

            // Limpieza básica: elimina escapes que rompen JSON
            text = text.Replace("\\\n", "").Replace("\\\r\n", "").Trim();

            return text;
        }
        catch (Exception e)
        {
            Debug.LogError("GeminiResponseCleaner failed: " + e.Message);
            return null;
        }
    }

    // ===== CLASES AUXILIARES PARA DESERIALIZAR EL WRAPPER =====
    [Serializable]
    private class GeminiWrapper
    {
        public Candidate[] candidates;
    }

    [Serializable]
    private class Candidate
    {
        public Content content;
    }

    [Serializable]
    private class Content
    {
        public Part[] parts;
    }

    [Serializable]
    private class Part
    {
        public string text;
    }
}
