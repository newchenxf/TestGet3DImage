using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get3DImage : MonoBehaviour
{
    private RenderTexture shortcutRenderTexture;
    private Camera cutImageCamera;
    // Start is called before the first frame update
    void Start()
    {
        cutImageCamera = GameObject.Find("CutImageCamera").GetComponent<Camera>();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void init()
    {
        shortcutRenderTexture = RenderTexture.GetTemporary(600, 600, 16, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        shortcutRenderTexture.enableRandomWrite = true;
        cutImageCamera.targetTexture = shortcutRenderTexture;
    }

    public void StartCutImage()
    {
        RenderTexture.active = shortcutRenderTexture;
        Texture2D drawTexture2D = new Texture2D(shortcutRenderTexture.width, shortcutRenderTexture.height, TextureFormat.RGB24, false);
        drawTexture2D.ReadPixels(new Rect(0, 0, shortcutRenderTexture.width, shortcutRenderTexture.height), 0, 0);
        drawTexture2D.Apply();

        TextureUtils.saveTextureToFile(drawTexture2D, "SaveShortcut");
    }

    private void OnDestroy()
    {
        RenderTexture.ReleaseTemporary(shortcutRenderTexture);
    }
}
