using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleShader : MonoBehaviour
{
    public Texture TitleTexture;
    private CanvasRenderer _canvasRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _canvasRenderer = GetComponent<CanvasRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        _canvasRenderer.SetTexture(TitleTexture);
        _audioSource.Play();
    }

}
