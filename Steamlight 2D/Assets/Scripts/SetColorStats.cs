using UnityEngine;


using System.Collections.Generic;

public class SetColorHSV : MonoBehaviour
{
    SpriteRenderer[] spritesRenderer;

    public List<ColorStats> colorStats;
    float hue;
    float saturation;
    float value;

    [Header("Targeted Colors")]
    [SerializeField] bool targetColorsOn = false;
    [SerializeField][Range(0, 255)] float targetedColorMinR = 25;
    [SerializeField][Range(0, 255)] float targetedColorMaxR = 255;
    [SerializeField][Range(0, 255)] float targetedColorMinG = 25;
    [SerializeField][Range(0, 255)] float targetedColorMaxG = 255;
    [SerializeField][Range(0, 255)] float targetedColorMinB = 25;
    [SerializeField][Range(0, 255)] float targetedColorMaxB = 255;

    [Header("Copied Color From List")]
    [SerializeField] int colorToFollow;
    void Awake()
    {
        spritesRenderer = FindObjectsOfType<SpriteRenderer>();
    }

    void GenerateNewColor()
    {
        hue = Random.Range(colorStats[colorToFollow].GetHueMinValue() / 360, colorStats[colorToFollow].GetHueMaxValue() / 360);
        saturation = Random.Range(colorStats[colorToFollow].GetSaturationMinValue() / 100, colorStats[colorToFollow].GetsSaturationMaxValue() / 100);
        value = Random.Range(colorStats[colorToFollow].GetValueMinValue() / 100, colorStats[colorToFollow].GetValueMaxValue() / 100);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (targetColorsOn)
            {
                TargetGroupOfColors();
            }

            if (!targetColorsOn)
            {
                SetColor();
            }
        }
    }
    void SetColor()
    {
        for (int i = 0; i < spritesRenderer.Length; i++)
        {
            GenerateNewColor();
            spritesRenderer[i].color = Color.HSVToRGB(hue, saturation, value);
        }
    }

    // if the value of the color is between the targeted value then change it
    void TargetGroupOfColors()
    {
        for (int i = 0; i < spritesRenderer.Length; i++)
        {
            if (spritesRenderer[i].color.r >= targetedColorMinR / 255f && spritesRenderer[i].color.r <= targetedColorMaxR / 255f && spritesRenderer[i].color.g >= targetedColorMinG / 255f && spritesRenderer[i].color.g <= targetedColorMaxG / 255f && spritesRenderer[i].color.b >= targetedColorMinB / 255f && spritesRenderer[i].color.b <= targetedColorMaxB / 255f)
            {
                GenerateNewColor();
                spritesRenderer[i].color = Color.HSVToRGB(hue, saturation, value);
                Debug.Log(spritesRenderer[i].name);
            }
        }
    }
}

[System.Serializable]
public class ColorStats
{
    [SerializeField]
    public string name;
    public float hueMinValue = 0;
    public float hueMaxValue = 360;
    public float saturationMinValue = 0;
    public float saturationMaxValue = 100;
    public float valueMinValue = 0;
    public float valueMaxValue = 100;

    public string GetName()
    {
        return name;
    }

    public float GetHueMinValue()
    {
        return hueMinValue;
    }

    public float GetHueMaxValue()
    {
        return hueMaxValue;
    }
    public float GetSaturationMinValue()
    {
        return saturationMinValue;
    }

    public float GetsSaturationMaxValue()
    {
        return saturationMaxValue;
    }
    public float GetValueMinValue()
    {
        return valueMinValue;
    }

    public float GetValueMaxValue()
    {
        return valueMaxValue;
    }
}

