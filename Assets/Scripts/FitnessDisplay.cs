using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class FitnessDay
{
    public string date; // Date of the day (format ex: "Tuesday, 09 Apr")
    public int steps; // Number of steps
    public float distanceKm; // Distance in kilometers
}

/// <summary>
/// The main script that generates and displays fitness data in the UI.
/// Creates a visual card for each day, including a total card.
/// </summary>
public class FitnessDisplay : MonoBehaviour
{
    public Transform contentParent; // The parent (Content from ScrollView) where the cards will be instantiated
    public GameObject fitnessCardPrefab; // Prefab of each card containing date, steps and distance
    public Button buttonRegenerate; // Button to regenerate data
    private List<GameObject> currentEntries = new List<GameObject>(); // Current list of instantiated inputs/cards

    /// <summary>
    /// Executed when the scene starts. Generates initial data and sets the listener for the button.
    /// </summary>
    private void Start()
    {
        GenerateAndShowData();

        // Set the regeneration function when the button is pressed
        if (buttonRegenerate != null)
        {
            buttonRegenerate.onClick.RemoveAllListeners();
            buttonRegenerate.onClick.AddListener(() =>
            {
                Regenerate();
            });
        }
    }

    /// <summary>
    /// Deletes all existing cards and generates a new data set.
    /// </summary
    private void Regenerate()
    {
        foreach (var entry in currentEntries)
        {
            // Destroy the GameObject in the UI
            Destroy(entry);
        }

        // Empty the internal list
        currentEntries.Clear();
        // Generate again
        GenerateAndShowData();
    }

    /// <summary>
    /// Creates UI objects for each day and displays a summary card at the end.
    /// </summary>
    private void GenerateAndShowData()
    {
        List<FitnessDay> data = GenerateFakeData(); // Simulate the data
        int totalSteps = 0;
        float totalDist = 0f;

        // Create a card for each day
        foreach (var day in data)
        {
            GameObject entry = Instantiate(fitnessCardPrefab, contentParent);
            FitnessEntryUI entryUI = entry.GetComponent<FitnessEntryUI>();
            entryUI.SetData(day); // Set the data on the UI
            currentEntries.Add(entry);

            totalSteps += day.steps;
            totalDist += day.distanceKm;
        }

        // Create an additional card for the total
        GameObject totalCard = Instantiate(fitnessCardPrefab, contentParent);
        FitnessEntryUI totalUI = totalCard.GetComponent<FitnessEntryUI>();
        totalUI.dateText.text = "Total";
        totalUI.stepsText.text = $"{totalSteps} steps";
        totalUI.distanceText.text = $"{totalDist:F1} km";
        currentEntries.Add(totalCard);
    }

    /// <summary>
    /// Generates a 7-day list of simulated fitness data.
    /// </summary>
    /// <returns>A list of FitnessDay objects</returns>
    List<FitnessDay> GenerateFakeData()
    {
        var days = new List<FitnessDay>();
        for (int i = 6; i >= 0; i--)
        {
            days.Add(new FitnessDay
            {
                date = System.DateTime.Now.AddDays(-i).ToString("dddd, dd MMM"),
                steps = Random.Range(4000, 12000),
                distanceKm = Random.Range(2.5f, 10f)
            });
        }
        return days;
    }
}