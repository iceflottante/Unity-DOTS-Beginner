using UnityEngine;

public class CounterDisplay : MonoBehaviour
{
    private static int count = 0;
    
    public static int Count
    {
        get => count;
    }

    private void OnGUI()
    {
        var w = Screen.width;
        var h = Screen.height;

        var rect = new Rect(0, 20, w, 20);

        var text = $"number: {count:0}";
        
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

    public static void Add(int num)
    {
        count += num;
    }

    public static void Set(int num)
    {
        count = num;
    }
}