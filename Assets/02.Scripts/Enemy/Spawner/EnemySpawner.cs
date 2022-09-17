using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnFair
{
    public GameObject prefab;
    public int value;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<SpawnFair> spawnList = new List<SpawnFair>(); // 생성될 몬스터와 각 가중치가 젛여 있는 리스트

    [SerializeField]
    private int totalMonsterCount = 5; // 총합 존재할 수 있는 몬스터 수

    private Dictionary<string, List<GameObject>> monsterDic = new Dictionary<string, List<GameObject>>(); // 현재 생성된 몬스터들을 몬스터의 이름을 키값으로하는 넣는 딕셔너리

    private float spawnTimer = 0f;
    [SerializeField]
    private float spawnTime = 2f;

    private void Start()
    {
        
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnTime)
        {
            int monsterCnt = 0;
            foreach(KeyValuePair<string, List<GameObject>> list in monsterDic)
            {
                monsterCnt += list.Value.Count;
            }

            if(monsterCnt < totalMonsterCount)
            {
                CreateMonster();
            }
        }
    }

    private void CreateMonster()
    {
        // 몬스터 생성 함수
    }
}
