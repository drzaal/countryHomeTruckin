using UnityEngine;

public struct LevelStats
{
    public float cleartime;
    public int chickens;
    public int pigs;
    public int corn;
    public int cows;
    public int pumpkins;
    public int turkeys;
    public int potatoes;
    public int cabbages;

    public int toSumPoints() {
        Debug.Log(JsonUtility.ToJson(this));
        int sum = pigs + chickens + corn + cows + pumpkins + turkeys + cabbages + potatoes;

        Debug.Log(sum);
        return sum; 
    }
    public float toStdPoints() {
        float std = stdDev(new float[] { pigs, chickens, corn, cows, pumpkins, turkeys, cabbages, potatoes});
        return 1 - std;
    }

    public float stdDev(float[] input) {
        int n = input.Length;
        float sum = 0f;
        float avg = 0f;
        float std = 0f;
        if (n == 0) return 0;
        for (int i=0;i<n;i++) sum += input[i];
        avg = sum/ n;
        for (int i=0;i<n;i++) std += Mathf.Pow(avg - input[i], 2);

        return std/n;
    }
}