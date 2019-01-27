using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawStatus : MonoBehaviour {

    [SerializeField] enum Happiness { HAPPY, MILD, ANGRY, FURIOUS }
    Happiness happiness;

    [SerializeField] UnityEngine.Sprite happyImage;
    [SerializeField] UnityEngine.Sprite mildImage;
    [SerializeField] UnityEngine.Sprite angryImage;
    [SerializeField] UnityEngine.Sprite furyImage;

    [SerializeField] Image iconImage;
    [SerializeField] GameHud gameHud;

    void Update()
    {
        happiness = getHappiness();
    }

    Happiness getHappiness()
    {
        if (gameHud == null) return Happiness.FURIOUS;
        if (GameManager.instance.foodSum >= GameManager.instance.feastMinimum) return Happiness.HAPPY;
        if (GameManager.instance.foodSum >= GameManager.instance.mealMinimum) return Happiness.MILD;
        return Happiness.FURIOUS;
    }

    void OnGUI()
    {
        if (iconImage != null)
        {
        switch (happiness)
        {
            case Happiness.HAPPY: iconImage.sprite = happyImage;
                break;
            case Happiness.MILD: iconImage.sprite = mildImage;
                break;
            case Happiness.ANGRY: iconImage.sprite = angryImage;
                break;
            default: iconImage.sprite = furyImage;
                break;
        }
        }
    }

}