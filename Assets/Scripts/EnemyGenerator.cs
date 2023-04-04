using UnityEngine;
using UnityEngine.UIElements;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossEnemyPrefab;
    private int currentEnemyCount = 0; // 現在の敵数

    // Start is called before the first frame update
    void Start()
    {
        // 繰り返し関数を実行する
        InvokeRepeating("Spawn", 2f, 0.5f); // Spawn関数を2秒後に0.5秒毎に実行する
        //Invoke("BossSpawn", 4f);
    }

    // 生成する
    void Spawn()
    {
        // 現在の敵数が最大敵数より少なければ生成する
        if (currentEnemyCount < Const.COUNT.CONST_MAX_ENEMY)
        {
            // 画面端のx座標を求める
            float screenX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;

            // ランダムに左右を決める
            int direction = Random.Range(0, 2) == 0 ? -1 : 1;

            // 生成する位置を決定する
            Vector3 spawnPosition = new Vector3(
                screenX * direction,  // 画面端のx座標にランダムに左右を決めた値を掛けて出現位置を決定
                transform.position.y - 2f,
                transform.position.z
            );

            Instantiate(
                enemyPrefab,        // 生成するオブジェクト
                spawnPosition, // 生成時の位置
                transform.rotation  // 生成時の向き
            );

            // 現在の敵数を1増やす
            currentEnemyCount++;
        }
    }

    void BossSpawn()
    {
        Instantiate(
            bossEnemyPrefab,    // 生成するオブジェクト
            transform.position, // 生成時の位置
            transform.rotation  // 生成時の向き
        );
        CancelInvoke();
    }

    public void OnEnemyDestroyed()
    {
        // 敵が撃破されたら、現在の敵数を1減らす
        currentEnemyCount--;
    }

    public void DivededSpawn(GameObject enemy, Vector3 position)
    {
        GameObject rightEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        rightEnemy.transform.localScale = enemy.transform.localScale * 0.8f;
        // 敵が撃破されたら、現在の敵数を1増やす
        currentEnemyCount++;
        GameObject leftEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        leftEnemy.transform.localScale = enemy.transform.localScale * 0.8f;
        // 敵が撃破されたら、現在の敵数を1増やす
        currentEnemyCount++;

        // 敵に速度を与える
        Rigidbody2D rightRigidbody = rightEnemy.GetComponent<Rigidbody2D>();
        Rigidbody2D leftRigidbody = leftEnemy.GetComponent<Rigidbody2D>();

        rightRigidbody.AddForce(Vector2.right * 2.0f, ForceMode2D.Impulse);
        leftRigidbody.AddForce(Vector2.left * 2.0f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
