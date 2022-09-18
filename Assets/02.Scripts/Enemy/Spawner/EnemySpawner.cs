using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

[Serializable]
class SpawnFair
{
    public GameObject prefab;
    public int rate;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<SpawnFair> spawnList = new List<SpawnFair>(); // 생성될 몬스터와 각 가중치가 젛여 있는 리스트

    [SerializeField]
    private int totalMonsterCount = 5; // 총합 존재할 수 있는 몬스터 수
    [ShowNonSerializedField]
    private int currentMonsterCount;

    private Dictionary<string, List<GameObject>> monsterDic = new Dictionary<string, List<GameObject>>(); // 현재 생성된 몬스터들을 몬스터의 이름을 키값으로하는 넣는 딕셔너리

    private float spawnTimer = 0f;
    [SerializeField]
    private float spawnTime = 2f;

    private void Start()
    {
        for(int i = 0; i < spawnList.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            monsterDic.Add(spawnList[i].prefab.name, list);
        }
    }

    private void Update()
    {
        if(currentMonsterCount < totalMonsterCount)
            spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnTime)
        {
            //currentMonsterCount = 0;
            //foreach(KeyValuePair<string, List<GameObject>> list in monsterDic)
            //{
            //    currentMonsterCount += list.Value.Count;
            //}

            if(currentMonsterCount < totalMonsterCount)
            {
                CreateMonster();
                spawnTimer = 0f;
                currentMonsterCount++;
            }
        }
    }

    private void CreateMonster()
    {
        // 몬스터 생성 함수
        string monsterName = spawnList[GetRandomMonsterIndex()].prefab.name;
        Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, 0);
        Enemy enemy = PoolManager.Instance.Pop(monsterName) as Enemy;
        enemy.transform.position = this.transform.position + offset;

        monsterDic[monsterName].Add(enemy.gameObject);
    }

    private int GetRandomMonsterIndex()
    {
        int sum = 0;
        for (int i = 0; i < spawnList.Count; i++)
        {
            sum += spawnList[i].rate;
        }

        int randomValue = Random.Range(0, sum + 1);
        int tempSum = 0;

        for (int i = 0; i < spawnList.Count; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + spawnList[i].rate)
            {
                return i;
            }
            else
            {
                tempSum += spawnList[i].rate;
            }
        }

        return 0;
    }

    public void RemoveMonster(string name)
    {
        // 딕셔너리에서 삭제
        if(monsterDic.ContainsKey(name) == true)
        {
            monsterDic[name].RemoveAt(0); // 0번 인덱스에 Enemy를 없에는게 맞나?
        }
        
        currentMonsterCount--;
    }
}
