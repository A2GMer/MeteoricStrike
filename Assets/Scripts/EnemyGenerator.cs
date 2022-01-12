using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossEnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // 繰り返し関数を実行する
        InvokeRepeating("Spawn", 2f, 0.5f); // Spawn関数を2秒後に0.5秒毎に実行する
        Invoke("BossSpawn", 4f);
    }

    // 生成する
    void Spawn()
    {
        // 生成する位置（x座標）をランダムにしたい
        Vector3 spawnPosition = new Vector3(
            Random.Range(-2.5f,2.5f),
            transform.position.y,
            transform.position.z
            );

        Instantiate(
            enemyPrefab,        // 生成するオブジェクト
            spawnPosition, // 生成時の位置
            transform.rotation  // 生成時の向き
            );
    }

    void BossSpawn()
    {
        Instantiate(
            bossEnemyPrefab,        // 生成するオブジェクト
            transform.position, // 生成時の位置
            transform.rotation  // 生成時の向き
            );
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
