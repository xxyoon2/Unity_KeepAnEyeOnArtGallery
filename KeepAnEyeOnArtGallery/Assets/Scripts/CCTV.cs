using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCTV : MonoBehaviour
{
    public Texture[] CameraTextures;

    private int _currentTextureIndex = 0;
    private CanvasRenderer _canvasRenderer;

    private void Awake()
    {
        _canvasRenderer = GetComponent<CanvasRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentTextureIndex = (_currentTextureIndex + 1) % CameraTextures.Length;
            _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
            GameManager.Instance.CameraIndexTest(_currentTextureIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentTextureIndex = (_currentTextureIndex - 1) % CameraTextures.Length;
            if (_currentTextureIndex < 0)
                _currentTextureIndex = CameraTextures.Length;
            _canvasRenderer.SetTexture(CameraTextures[_currentTextureIndex]);
            GameManager.Instance.CameraIndexTest(_currentTextureIndex + 1);
        }
        
    }
}
