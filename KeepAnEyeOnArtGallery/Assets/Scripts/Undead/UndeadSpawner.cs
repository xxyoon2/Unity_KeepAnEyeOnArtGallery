using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadSpawner : MonoBehaviour
{
    public Transform[] SpawnTransform;
    public GameObject UndeadPrefab;
    private GameObject[] _undead = new GameObject[10];

    private int _undeadCount = -1;

    void Awake()
    {
        GameManager.Instance.SpawnUndead.RemoveListener(Spawn);
        GameManager.Instance.SpawnUndead.AddListener(Spawn);

        GameManager.Instance.RemoveUndead.RemoveListener(Deactive);
        GameManager.Instance.RemoveUndead.AddListener(Deactive);
    }

    /*
    public void Init(GameObject undead)
    {
        IsActive = false;
        _undead = undead;
        _undead.gameObject.SetActive(false);
    }
    */

    public void Spawn(int undeadSpawnPos)
    {
        GameObject undead = Instantiate<GameObject>(UndeadPrefab);

        ++_undeadCount;
        _undead[_undeadCount] = undead;
        
        if (_undeadCount > 2)
        {
            Debug.Log("님죽음ㅅㄱ");
        }

        _undead[_undeadCount].transform.position = SpawnTransform[undeadSpawnPos].position;

        Debug.Log($"{_undead[_undeadCount]}를 {_undead[_undeadCount].transform.position}에 소환했습니다.");
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

    public void Deactive()
    {
        Destroy(_undead[_undeadCount--]);
    }
}
