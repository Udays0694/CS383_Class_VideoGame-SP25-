/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;
using UnityEngine.Rendering.VirtualTexturing;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    [SerializeField] private float flickerSpeed = 0.1f;
    [SerializeField] private float intensityMin = 0.8f;
    [SerializeField] private float intensityMax = 1.2f;

    private void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();

        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    private void Flicker()
    {
        light2D.intensity = Random.Range(intensityMin, intensityMax);
    }
}
*/