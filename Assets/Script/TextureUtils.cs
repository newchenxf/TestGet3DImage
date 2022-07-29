using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    class TextureUtils
    {
        public static Texture2D DeCompress(Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }


        public static string saveTextureToFile(Texture2D texture, string fileName)
        {
            if (texture == null)
            {
                return "";
            }

            byte[] bytes = TextureUtils.DeCompress(texture).EncodeToPNG();
            string filename = Application.persistentDataPath + "/" + fileName + ".png";
            Debug.Log("DrawManager::saveTextureToFile = " + filename);
            File.WriteAllBytes(filename, bytes);
            return filename;
        }

    }
}
