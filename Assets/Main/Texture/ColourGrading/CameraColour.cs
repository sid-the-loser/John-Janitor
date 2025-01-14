using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenCameraShader : MonoBehaviour
{
//public Shader awesomeShader = null;
    public Material ColorLUTShader;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, ColorLUTShader);
    }
}