using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeristentManagers : MonoBehaviour
{
    // CONFIG DATA
    [Tooltip("This prefab will only be spawned once and persisted between " +
    "scenes.")]
    [SerializeField] GameObject persistentObjectPrefab = null;

    // PRIVATE STATE
    static bool hasSpawned = false;

    // PRIVATE
    private void Awake()
    {
        if (hasSpawned) return;

        SpawnPersistentObjects();

        hasSpawned = true;
    }

    private void SpawnPersistentObjects()
    {
        DontDestroyOnLoad(Instantiate(persistentObjectPrefab));
    }
}
