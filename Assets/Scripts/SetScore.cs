using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    [SerializeField] private Text valueText = null;

    public void AddScore(int value)
    {
        Score.value += value;
        valueText.text = Score.value.ToString();
    }
}
