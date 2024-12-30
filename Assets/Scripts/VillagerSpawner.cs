using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : MonoBehaviour
{
    [Header("Villager Settings")]
    public List<GameObject> villagerPrefabs; // List of villager prefabs to choose from
    [SerializeField]
    private int numberOfVillagers = 20; // Number of villagers to spawn

    [Header("Spawn Points")]
    public Transform spawnPointsParent; // Parent object containing spawn points
    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        // Populate the spawn points list
        foreach (Transform child in spawnPointsParent)
        {
            spawnPoints.Add(child);
        }

        // Spawn villagers
        for (int i = 0; i < numberOfVillagers; i++)
        {
            SpawnVillager();
        }
    }

    void SpawnVillager()
    {
        // Randomly select a villager prefab
        GameObject randomPrefab = villagerPrefabs[Random.Range(0, villagerPrefabs.Count)];

        // Randomly select a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Instantiate villager at the spawn point
        GameObject villager = Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);

        // Pass the spawn points to the VillagerMovement script
        VillagerMovement villagerMovement = villager.GetComponent<VillagerMovement>();
        if (villagerMovement != null)
        {
            villagerMovement.SetPoints(spawnPoints);
        }
    }
}
