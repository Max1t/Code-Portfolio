using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class CameraRenderTex : MonoBehaviour
{

    public Camera camera2;
    RenderTexture otherTexture;
    public PostProcessProfile _activeVolume;


    public void Start()
    {
        camera2.targetTexture = new RenderTexture(Screen.width, Screen.height, 32);

        otherTexture = camera2.targetTexture;

        SplitScreen _renderSettings = null;
        _activeVolume.TryGetSettings(out _renderSettings);
        _renderSettings._Tex = new TextureParameter { value = otherTexture };
    }

    void Update()
    {

    }
}
