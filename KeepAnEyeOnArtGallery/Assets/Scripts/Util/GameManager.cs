using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public struct MoveableObject
{
    public GameObject Name;
    public int RoomNum;
    public bool IsActive;
    public bool IsUndeadLive;
    public int ModifiedOption;
}

public class GameManager : SingletonBehavior<GameManager>
{
#region CCTV 관련
    public UnityEvent<int> ShowCamInfo = new UnityEvent<int>();

    public void CameraIndexTest(int index)
    {
        ShowCamInfo.Invoke(index);
    }

#endregion

#region UI 관련
    public UnityEvent CanUpdateAnomaly = new UnityEvent();
    public UnityEvent<string> NotifyTextChange = new UnityEvent<string>();

    public bool IsPlayerWatchingCCTV = false;

    public void UpdateNotifyText(string hitObjInfo)
    {
        NotifyTextChange.Invoke(hitObjInfo);
    }

#endregion

#region Object 변화 관련
    // 오브젝트 변화 주는 이벤트
    public UnityEvent<GameObject> ChangeObjectPosition = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> ChangeObjectRotation = new UnityEvent<GameObject>();
    
    // 오브젝트 고치는 이벤트
    public UnityEvent<GameObject, int> AnomalyFix = new UnityEvent<GameObject, int>();

    // 오브젝트 관리 관련
    public GameObject Showrooms;
    public MoveableObject[] Objects = new MoveableObject[18];

    public int ObjectTotalCount = 0;    // 오브젝트 개수
    public int ActiveObjectCount = 0;   // 활성화된 오브젝트 개수

    public int result;

    private void UpdateAnomaly()
    {
        int targetObj = SelectRandomObj();

        switch (Random.Range(0, 2))
        {
            // 위치 이동
            case 0:
                ChangeObjectPosition.Invoke(Objects[targetObj].Name);
                Objects[result].ModifiedOption = 0;
                break;
            // 회전
            case 1:
                ChangeObjectRotation.Invoke(Objects[targetObj].Name);
                Objects[result].ModifiedOption = 1;
                break;
        }

        ++ActiveObjectCount;
    }

    private int SelectRandomObj()
    {
        result = -1;
        while(result == -1)
        {
            int randomObject = Random.Range(0, ObjectTotalCount);   // 랜덤 오브젝트 설정
            if (Objects[randomObject].IsActive == false)
            {
                Objects[randomObject].IsActive = true;
                result = randomObject;
                Debug.Log($"저는{result}입니다");
            }
        }
        Debug.Log($"{Objects[result].Name}변경. {ActiveObjectCount}개의 오브젝트 활성화 됨");

        return result;
    }

    public void UpdateRayTarget(GameObject target)
    {
        findAndObject(target);
    }

    private int findAndObject(GameObject target)
    {
        for (int i = 0; i < ObjectTotalCount; ++i)
        {
            if (target.name == Objects[i].Name.name)
            {
                if (Objects[i].IsActive)
                {
                    AnomalyFix.Invoke(target, i);

                    if (Objects[i].IsUndeadLive)
                    {
                        RemoveUndead.Invoke();
                    }

                    Objects[i].IsActive = false;
                    Objects[i].IsUndeadLive = false;
                    --ActiveObjectCount;
                    GameManager.Instance.UpdateNotifyText("FixCompleted");
                    return i;
                }
            }
        }
        GameManager.Instance.UpdateNotifyText("FixFailed");
        return -1;
    }

#endregion

#region Undead 관련
    public UnityEvent<int> SpawnUndead = new UnityEvent<int>();
    public UnityEvent RemoveUndead = new UnityEvent();

    private void spawnUndead()
    {
        Objects[result].IsUndeadLive = true;
        SpawnUndead.Invoke(Objects[result].RoomNum);
    }

#endregion

    void Start()
    {
        // 오브젝트 배열 생성
        Showrooms = GameObject.Find("MoveableObjects");
        int roomCount = Showrooms.transform.childCount;
        for (int i = 0; i < roomCount; ++i)
        {
            GameObject room = Showrooms.transform.GetChild(i).gameObject;
            
            int objectCount = room.transform.childCount;
            for (int j = ObjectTotalCount; j < ObjectTotalCount + objectCount; ++j)
            {
                GameObject obj = room.transform.GetChild(j - ObjectTotalCount).gameObject;
                Objects[j].Name = obj;
                Objects[j].IsActive = false;
                Objects[j].RoomNum = i;
                Objects[j].IsUndeadLive = false;
                Objects[j].ModifiedOption = -1;
            }
            
            ObjectTotalCount += objectCount;
        }

        StartCoroutine("GameUpdate");
    }

    private float _elapsedTime;
    private int _anomalyCooltime = 30;

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        // 시간이 업데이트 될 때마다 오브젝트 변화사항도 하나씩 추가됨
        if (_elapsedTime >= _anomalyCooltime)
        {
            // 시간 업데이트
            _elapsedTime = 0f;
            CanUpdateAnomaly.Invoke();
        }
    }

    IEnumerator GameUpdate()
    {
        yield return new WaitForSeconds(30f);

        while (true)
        {
            UpdateAnomaly();    // 오브젝트 변화
            int num = result;
        
            yield return new WaitForSeconds(20f);

            Debug.Log($"{num}이었는데");
            if (Objects[num].IsActive)
            {
                Debug.Log($"{num}입니다");
                spawnUndead();  // 언데드 소환
                Objects[num].IsUndeadLive = true;

            }

            yield return null;
        }
        
    }

    public void OnGameEnd()
    {
        StopCoroutine("GameUpdate");
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
}