using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour {

    [SerializeField] public float timeToDinner;
    [SerializeField] public float unhappyTime;
    [SerializeField] public float angryTime;
    [SerializeField] public float divorceTime;

    [SerializeField] float truckCondition;
    [SerializeField] int mealValue;


    [SerializeField] Text travelTimeDisplayText;

    void Update()
    {
        timeToDinner += Time.deltaTime;
    }

    void OnGUI()
    {
        if (travelTimeDisplayText != null)
        {
            travelTimeDisplayText.text = floatToTime();
        }
    }

    public string floatToTime()
    {
        int minutes = (int)(timeToDinner / 60f);
        int seconds = (int) timeToDinner % 60;
        
        int milliSeconds = (int) (timeToDinner * 100) % 100;

        string display = "";
        if (minutes > 0) 
        {
            display += minutes + " : ";
            display += seconds.ToString("D2");
        }
        else
        {
            display += seconds.ToString();
        }
        if (milliSeconds > 0) display += "." + milliSeconds.ToString("D2");
        else display += "   ";

        return display;
    }
}