using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour {


    [SerializeField] Text travelTimeDisplayText;
    [SerializeField] Text foodDisplayValue;
    [SerializeField] Text nutritionDisplayValue;


    [SerializeField] Image travelTimeDisplaySprite;
    [SerializeField] Image foodDisplaySprite;
    [SerializeField] Image nutritionDisplaySprite;

    [SerializeField] float secondsForAnimation;

    float animationProgress;

    void Update()
    {
        float animationChange = secondsForAnimation > 0 ? Time.deltaTime / secondsForAnimation : 1;
        if (!GameManager.instance.haveWon)
        {
            animationProgress = Mathf.Min(1, animationProgress + animationChange);
        }
        else
        {
            animationProgress = Mathf.Max(0, animationProgress - animationChange);
        }
    }

    void OnGUI()
    {
        
        if (travelTimeDisplayText != null)
        {
            travelTimeDisplaySprite.color = new Color
            {
                r = travelTimeDisplaySprite.color.r,
                g = travelTimeDisplaySprite.color.g,
                b = travelTimeDisplaySprite.color.b,
                a = animationProgress
            };
            travelTimeDisplayText.color = new Color
            {
                r = travelTimeDisplayText.color.r,
                g = travelTimeDisplayText.color.g,
                b = travelTimeDisplayText.color.b,
                a = animationProgress
            };
            travelTimeDisplayText.text = GameManager.instance.floatToTime();
        }
        if (nutritionDisplayValue != null)
        {
            nutritionDisplayValue.color = new Color
            {
                r = nutritionDisplayValue.color.r,
                g = nutritionDisplayValue.color.g,
                b = nutritionDisplayValue.color.b,
                a = animationProgress
            };
            nutritionDisplaySprite.color = new Color
            {
                r = nutritionDisplaySprite.color.r,
                g = nutritionDisplaySprite.color.g,
                b = nutritionDisplaySprite.color.b,
                a = animationProgress
            };
            nutritionDisplayValue.text = GameManager.instance.levelStats.toCubeRtSum().ToString();
        }
        if (foodDisplayValue != null)
        {
            foodDisplayValue.color = new Color
            {
                r = foodDisplayValue.color.r,
                g = foodDisplayValue.color.g,
                b = foodDisplayValue.color.b,
                a = animationProgress
            };
            foodDisplaySprite.color = new Color
            {
                r = foodDisplaySprite.color.r,
                g = foodDisplaySprite.color.g,
                b = foodDisplaySprite.color.b,
                a = animationProgress
            };
            foodDisplayValue.text = GameManager.instance.levelStats.toSumPoints().ToString();
        }
        
    }
}