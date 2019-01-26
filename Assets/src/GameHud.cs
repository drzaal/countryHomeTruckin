using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHud : MonoBehaviour {

    [SerializeField] float timeToDinner;
    [SerializeField] float truckCondition;
    [SerializeField] int mealValue;

    void Update()
    {

    }

    public string floatToTime()
    {
        int minutes = (int)(timeToDinner / 60f);
        int seconds = (int) timeToDinner % 60;
        
        int milliSeconds = (int) (timeToDinner * 100) % 100;

        string display = "";
        if (minutes > 0) display += minutes + " : ";
        display += seconds.ToString();
        if (milliSeconds > 0) display += "." + milliSeconds.ToString();
        
        return display;
    }
}