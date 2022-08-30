using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSpawner : MonoBehaviour
{
    public Transform[] SpawnTransform;
    public bool IsActive { get; private set; }
    private GameObject _undead;


    public void Init(GameObject undead)
    {
        IsActive = false;
        _undead = undead;
        _undead.gameObject.SetActive(false);
    }

    public void Spawn(int undeadNum)
    {
        Debug.Assert(GameManager.Instance.Objects[undeadNum].IsActive == false);

        StartCoroutine(spawnHelper(undeadNum));        
    }

    private IEnumerator spawnHelper(int undeadNum)
    {
        yield return new WaitForSeconds(10f);

        if (IsActive)
        {
            _undead.GetComponent<Transform>().position = SpawnTransform[GameManager.Instance.Objects[undeadNum].RoomNum].position;
            _undead.gameObject.SetActive(true);
        }
    }

    public void Deactive()
    {
        if (IsActive == false)
        {
            return;
        }

        IsActive = false;
        _undead.gameObject.SetActive(false);
    }



    /*

    //public GameObject UndeadPrefab;
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
    */
}
