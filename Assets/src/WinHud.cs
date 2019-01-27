using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinHud : MonoBehaviour {

    [SerializeField] Image screenPanel;
    [SerializeField] Text travelTimeDisplayText;
    [SerializeField] Text foodDisplayValue;
    [SerializeField] Text nutritionDisplayValue;
    [SerializeField] Image winImage;
    [SerializeField] Image mediocreImage;
    [SerializeField] Image loserImage;

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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnGUI()
    {

        screenPanel.transform.position = new Vector3(
            (0.5f - animationProgress) * Screen.width,
            screenPanel.transform.position.y,
            screenPanel.transform.position.z
        );
        winImage.enabled = false;
        loserImage.enabled = false;
        mediocreImage.enabled = false;

        int winStatus = 0;
        winStatus += GameManager.instance.timeToDinner < GameManager.instance.divorceTime ? 1 : 0;
        winStatus += GameManager.instance.foodSum >= GameManager.instance.feastMinimum ? 1 : 0;
        winStatus += GameManager.instance.mealNutrition >= GameManager.instance.roundedMeal ? 1 : 0;

        if (winStatus == 0) loserImage.enabled = true;
        else if (winStatus > 1) winImage.enabled = true;
        else mediocreImage.enabled = true;

        if (animationProgress > 0)
        {
            if (travelTimeDisplayText != null)
            {
                travelTimeDisplayText.text = GameManager.instance.floatToTime();
            }
            if (nutritionDisplayValue != null)
            {
                nutritionDisplayValue.text = GameManager.instance.mealNutrition + " Variety";
            }
            if (foodDisplayValue != null)
            {
                foodDisplayValue.text = GameManager.instance.foodSum + " Pounds";
            }
        }
    }
}