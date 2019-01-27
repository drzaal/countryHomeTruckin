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
        int sum = pigs + chickens + corn + cows + pumpkins + turkeys + cabbages + potatoes;

        return sum; 
    }
    public float toCubeRtSum() {
        float std = rt3Sum(new float[] { pigs, chickens, corn, cows, pumpkins, turkeys, cabbages, potatoes});
        return std;
    }

    public float rt3Sum(float[] input) {
        int n = input.Length;
        float sum = 0f;
        if (n == 0) return 0;
        for (int i=0;i<n;i++) {
            sum += (input[i] > 0f ? Mathf.Pow(input[i], 0.333f) : 0f);
        }

        return sum;
    }
}