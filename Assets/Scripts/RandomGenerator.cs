using UnityEngine;

public static class RandomGenerator
{
    /// <summary> chance to get true 1 : "probability" </summary>
    public static bool GetRandomBool(int probability)
    {
        return Random.Range(0, probability) == 1;
    }
}
