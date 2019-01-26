public struct LevelStats
{
    public float cleartime;
    public int chickens;
    public int corn;
    public int cows;
    public int pumpkins;

    public int toPoints() {
        return chickens + corn + cows + pumpkins - (int) cleartime; 
    }
}