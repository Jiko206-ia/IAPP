using UnityEngine;
using System.Collections.Generic;

public class ModelerManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public void SpawnObject(string type)
    {
        GameObject prefab = type.ToLower() switch
        {
            "cube" => cubePrefab,
            "sphere" => spherePrefab,
            "cylinder" => cylinderPrefab,
            _ => null
        };

        if (prefab != null)
        {
            GameObject go = Instantiate(prefab, Camera.main.transform.position + Camera.main.transform.forward * 0.5f, Quaternion.identity);
            _spawnedObjects.Add(go);
        }
    }

    public void ClearAll()
    {
        foreach (var obj in _spawnedObjects) Destroy(obj);
        _spawnedObjects.Clear();
    }
}
