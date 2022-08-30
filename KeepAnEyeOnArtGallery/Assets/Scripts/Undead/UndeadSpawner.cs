using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSpawner : MonoBehaviour
{
    public Transform[] SpawnTransform;
    public bool IsActive { get; private set; }
    public GameObject UndeadPrefab;
    private GameObject[] _undead = new GameObject[10];

    void Awake()
    {
        GameManager.Instance.SpawnUndead.RemoveListener(Spawn);
        GameManager.Instance.SpawnUndead.AddListener(Spawn);
    }

    /*
    public void Init(GameObject undead)
    {
        IsActive = false;
        _undead = undead;
        _undead.gameObject.SetActive(false);
    }
    */

    public void Spawn(int undeadNum)
    {
        GameObject undead = Instantiate<GameObject>(UndeadPrefab);

        int index = GameManager.Instance.ActiveObjectCount;
        _undead[index] = undead;


        Debug.Assert(GameManager.Instance.Objects[undeadNum].IsActive == false);

        _undead[index].transform.position = SpawnTransform[undeadNum].position;

        Debug.Log($"{_undead[index]}를 {_undead[index].transform.position}에 소환했습니다.");
        //StartCoroutine(spawnHelper(undeadNum));        
    }
    

/*
    private IEnumerator spawnHelper(int undeadNum)
    {
        yield return new WaitForSeconds(10f);

        if (IsActive)
        {
            _undead.GetComponent<Transform>().position = SpawnTransform[undeadNum].position;
            _undead.gameObject.SetActive(true);
        }
    }
    */
/*
    public void Deactive()
    {
        if (IsActive == false)
        {
            return;
        }

        IsActive = false;
        _undead.gameObject.SetActive(false);
    }
*/


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
