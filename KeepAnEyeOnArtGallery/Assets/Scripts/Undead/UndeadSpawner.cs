using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSpawner : MonoBehaviour
{
    public GameObject UndeadPrefab;
    GameObject[] Spawners = new GameObject[3];
    void Start()
    {
        GameManager.Instance.SpawnUndead.RemoveListener(Spawn);
        GameManager.Instance.SpawnUndead.AddListener(Spawn);

        for (int i = 0; i < 3; i++)
        {
            Spawners[i] = transform.GetChild(i).gameObject;
            Debug.Log($"{Spawners[i]}");
        }
    }

    void Spawn(int roomNum)
    {
        GameObject undead = Instantiate(UndeadPrefab, Spawners[roomNum].transform);
    }
}
