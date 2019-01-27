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
        if (animationProgress > 0)
        {
            if (travelTimeDisplayText != null)
            {
                travelTimeDisplayText.text = GameManager.instance.floatToTime();
            }
            if (nutritionDisplayValue != null)
            {
                nutritionDisplayValue.text = GameManager.instance.mealNutrition.ToString();
            }
            if (foodDisplayValue != null)
            {
                foodDisplayValue.text = GameManager.instance.levelStats.toSumPoints().ToString();
            }
        }
    }
}