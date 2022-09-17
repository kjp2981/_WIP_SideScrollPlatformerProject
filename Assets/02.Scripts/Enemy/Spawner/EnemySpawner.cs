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
    private List<SpawnFair> spawnList = new List<SpawnFair>(); // ������ ���Ϳ� �� ����ġ�� ���� �ִ� ����Ʈ

    [SerializeField]
    private int totalMonsterCount = 5; // ���� ������ �� �ִ� ���� ��

    private Dictionary<string, List<GameObject>> monsterDic = new Dictionary<string, List<GameObject>>(); // ���� ������ ���͵��� ������ �̸��� Ű�������ϴ� �ִ� ��ųʸ�

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
        // ���� ���� �Լ�
    }
}
