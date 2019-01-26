using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpouseStatus : MonoBehaviour {

    [SerializeField] enum Happiness { HAPPY, MILD, ANGRY, FURIOUS }
    Happiness happiness;

    [SerializeField] Sprite happyImage;
    [SerializeField] Sprite mildImage;
    [SerializeField] Sprite angryImage;
    [SerializeField] Sprite furyImage;

    [SerializeField] Image iconImage;
    [SerializeField] GameHud gameHud;

    void Update()
    {
        happiness = getHappiness();
    }

    Happiness getHappiness()
    {
        if (gameHud == null) return Happiness.FURIOUS;
        if (gameHud.timeToDinner > gameHud.divorceTime) return Happiness.FURIOUS;
        if (gameHud.timeToDinner > gameHud.angryTime) return Happiness.ANGRY;
        if (gameHud.timeToDinner > gameHud.unhappyTime) return Happiness.MILD;
        return Happiness.HAPPY;
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