using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class FitnessDay
{
    public string date;
    public int steps;
    public float distanceKm;
}

public class FitnessDisplay : MonoBehaviour
{
    public Transform contentParent;
    public GameObject fitnessCardPrefab;
    public Button buttonRegenerate;
    private List<GameObject> currentEntries = new List<GameObject>();

    void Start()
    {
        GenerateAndShowData();

        if(buttonRegenerate != null)
        {
            buttonRegenerate.onClick.RemoveAllListeners();
            buttonRegenerate.onClick.AddListener(() =>
            {
                Regenerate();
            });
        }
    }

    private void Regenerate()
    {
        foreach (var entry in currentEntries)
        {
            Destroy(entry);
        }
        currentEntries.Clear();
        GenerateAndShowData();
    }

    void GenerateAndShowData()
    {
        List<FitnessDay> data = GenerateFakeData();
        int totalSteps = 0;
        float totalDist = 0f;

        foreach (var day in data)
        {
            GameObject entry = Instantiate(fitnessCardPrefab, contentParent);
            FitnessEntryUI entryUI = entry.GetComponent<FitnessEntryUI>();
            entryUI.SetData(day);
            currentEntries.Add(entry);

            totalSteps += day.steps;
            totalDist += day.distanceKm;
        }

        // Total card
        GameObject totalCard = Instantiate(fitnessCardPrefab, contentParent);
        FitnessEntryUI totalUI = totalCard.GetComponent<FitnessEntryUI>();
        totalUI.dateText.text = "Total";
        totalUI.stepsText.text = $"{totalSteps} steps";
        totalUI.distanceText.text = $"{totalDist:F1} km";
        currentEntries.Add(totalCard);
    }

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