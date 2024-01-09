using UnityEngine;
using UnityEngine.UI;

public static class Extensions
{
    public static string Color(this string content, string color)
    {
        return $"<color={color}>{content}</color>";
    }

    public static int ToMiliseconds(this float seconds)
    {
        return Mathf.RoundToInt(seconds * 1000);
    }

    public static void Fade(this Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
