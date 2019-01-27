using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public Transform bed;
    public Transform food;
	public float friction = 100f;
    [SerializeField] float gravity = 100f;
    [SerializeField] float maxFallSpeed = 25;


    // MawMaw's happiness
    [SerializeField] public float timeToDinner;
    [SerializeField] public float unhappyTime;
    [SerializeField] public float angryTime;
    [SerializeField] public float divorceTime;

    // Kid's happiness
    [SerializeField] public float mealNutrition;
    [SerializeField] public float roundedMeal;
    [SerializeField] public float wellRoundedMeal;

    // Kid's happiness
    [SerializeField] public float foodSum;
    [SerializeField] public float feastMinimum;
    [SerializeField] public float mealMinimum;

    [SerializeField] float truckCondition;
    [SerializeField] int mealValue;
    public bool haveWon = false;

    [SerializeField] public LevelStats levelStats;

	void Awake() {
		if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        haveWon = false;
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update()
    {
        if (!haveWon) timeToDinner += Time.deltaTime;
    }

    public void winLevel()
    {
        // Display win screen
        haveWon = true;
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