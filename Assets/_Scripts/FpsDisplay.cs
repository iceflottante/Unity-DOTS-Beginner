// https://wiki.unity3d.com/index.php/FramesPerSecond
using UnityEngine;


public class FpsDisplay : MonoBehaviour
{
    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int w = Screen.width,
            h = Screen.height;
 
        var rect = new Rect(0, 0, w, 20);
        
        var text = $"{deltaTime * 1000.0f:0.0} ms ({1.0f / deltaTime:0.} fps)";
        
        GUI.Label(
            rect,
            text, 
            new GUIStyle
            {
                alignment = TextAnchor.UpperLeft,
                fontSize = 20,
                normal =
                {
                    textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f),
                }
            });
    }
}
