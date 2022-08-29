using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCTV : MonoBehaviour
{
    public Texture[] CameraTextures;

    private int _currentTextureIndex = 0;
    private CanvasRenderer _canvasRenderer;
    private AudioSource _audioSource;

    private void Awake()
    {
        _canvasRenderer = GetComponent<CanvasRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentTextureIndex = (_currentTextureIndex + 1) % CameraTextures.Length;
            _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
            GameManager.Instance.CameraIndexTest(_currentTextureIndex + 1);
            _audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentTextureIndex = (_currentTextureIndex - 1) % CameraTextures.Length;
            _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
            GameManager.Instance.CameraIndexTest(_currentTextureIndex + 1);
            if (_currentTextureIndex <= 0)
                _currentTextureIndex = CameraTextures.Length;
            _audioSource.Play();
        }
        
    }
}
