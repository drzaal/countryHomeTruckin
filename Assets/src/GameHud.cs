using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour {


    [SerializeField] Text travelTimeDisplayText;
    [SerializeField] Text foodDisplayValue;
    [SerializeField] Text nutritionDisplayValue;

    void Update()
    {
    }

    void OnGUI()
    {
        if (travelTimeDisplayText != null)
        {
            travelTimeDisplayText.text = GameManager.instance.floatToTime();
        }
        if (nutritionDisplayValue != null)
        {
            nutritionDisplayValue.text = GameManager.instance.levelStats.toStdPoints().ToString();
        }
        if (foodDisplayValue != null)
        {
            foodDisplayValue.text = GameManager.instance.levelStats.toSumPoints().ToString();
        }
        
    }
}