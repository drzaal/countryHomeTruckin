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

    public int toSumPoints() {
        int sum = chickens + corn + cows + pumpkins + turkeys;

        return sum; 
    }
    public float toStdPoints() {
        float std = stdDev(new float[] { chickens, corn, cows, pumpkins, turkeys});
        return 1 - std;
    }

    public float stdDev(float[] input) {
        int n = input.Length;
        float sum = 0f;
        float avg = 0f;
        float std = 0f;
        if (n == 0) return 0;
        for (int i=0;i<n;i++) sum += input[n];
        avg = sum/ n;
        for (int i=0;i<n;i++) std += Mathf.Pow(avg - input[n], 2);

        return std/n;
    }
}