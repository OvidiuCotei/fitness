using TMPro;
using UnityEngine;

public class FitnessEntryUI : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI stepsText;
    public TextMeshProUGUI distanceText;

    public void SetData(FitnessDay data)
    {
        dateText.text = data.date;
        stepsText.text = $"{data.steps} steps";
        distanceText.text = $"{data.distanceKm:F1} km";
    }
}