using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class ClueLightController : MonoBehaviour
{
    public Light light;
    public float max_intensity = 0.1f;
    public float duration = 1.5f;

    public void EncenderLuzSuave(HoverEnterEventArgs informationSelect)
    {
        if (light.intensity > 0) return;
        StartCoroutine(AnimarLuz(0, max_intensity));
    }

    IEnumerator AnimarLuz(float begin, float end)
    {
        float time_elpased = 0;

        while (time_elpased < duration)
        {
            time_elpased += Time.deltaTime;
            float progress = time_elpased / duration;
            light.intensity = Mathf.Lerp(begin, end, progress);
            yield return null;
        }

        light.intensity = end;
    }
}
