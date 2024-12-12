using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    private List<GameObject> spawnedPlatesList;
    [SerializeField] private Transform platesPrefab;
    [SerializeField] private Transform counterTopPoint;

    private void Awake()
    {
        spawnedPlatesList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnSpawnPlate += PlatesCounter_OnSpawnPlate;
        platesCounter.OnDestroyPlate += PlatesCounter_OnDestroyPlate;
    }

    private void PlatesCounter_OnDestroyPlate(object sender, System.EventArgs e)
    {
        GameObject plate = spawnedPlatesList[spawnedPlatesList.Count - 1];
        spawnedPlatesList.Remove(plate);
        Destroy(plate);
    }

    private void PlatesCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        float offsetY = 0.1f;
        Transform plate = Instantiate(platesPrefab, counterTopPoint);
        plate.localPosition = new Vector3(0, 0 + offsetY * spawnedPlatesList.Count);
        spawnedPlatesList.Add(plate.gameObject);
    }
}
