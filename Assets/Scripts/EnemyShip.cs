using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 敵が弾を発射する
 * ・弾を作る
 * ・弾の移動を実装する
 * ・発射ポイントを作る
 * ・敵から弾を生成する
 * ・Playerが弾に当たったら破壊される
 * 　・弾にコライダーをつける
 * 　・敵が自分の弾に当たったら破壊されるバグの修正
 */

// 敵の移動：真下に移動する
// 敵を生成：生成工場を作る
// 敵に弾が当たったら爆発する
// 敵にPlayerが当たったら爆発する
public class EnemyShip : MonoBehaviour
{
    public GameObject explosion; // 爆発のプレファブ
    public GameObject bulletPrefab;

    // GameControllerの入れ物を作る：AddScoreを使いたから
    GameController gameController;
    EnemyGenerator enemyGenerator;
    float offset;
    void Start()
    {
        offset = Random.Range(0, 2f * Mathf.PI);
        //InvokeRepeating("Shot", 2f, 0.5f);
        // GameObject.Find("GameController");
        // ・ヒエラルキー上のGameControllerという名前のオブジェクトを取得
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // ・ヒエラルキー上のEnemyGeneratorという名前のオブジェクトを取得
        enemyGenerator = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
    }

    void Shot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // 敵の移動：真下に移動する
        //transform.position -= new Vector3(0,Time.deltaTime,0);

        // 敵が左右に移動する
        //transform.position -= new Vector3(
        //    Mathf.Cos(Time.frameCount * 0.02f + offset) * 0.01f,
        //    Time.deltaTime,
        //    0
        //    );

        // 敵の表示範囲
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
            // 敵数カウントを減らす
            enemyGenerator.OnEnemyDestroyed();
        }
    }

    // 敵に弾が当たったら爆発する
    // 当たり判定の基礎知識：当たり判定を行うには、
    // ・両者にColliderがついている
    // ・Rigidbodyがついている

    // トリガーにしていた場合、こちらが実行
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collisionにぶつかった相手の情報が入っている：bullet, Player
        if (collision.CompareTag("Player") == true)
        {
            Instantiate(explosion, collision.transform.position, transform.rotation);
            gameController.GameOver();
        }
        else if (collision.CompareTag("Bullet") == true)
        {
            // スコア加算
            gameController.AddScore();
        }
        else if (collision.CompareTag("EnemyBullet") == true)
        {
            return; // 何もしない
        }
        else if (collision.CompareTag("Ground") == true)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            ApplyForce(Vector2.up * 8f);
            return;
        }
        else if (collision.CompareTag("WallRight") == true)
        {
            ApplyForce(Vector2.left * 3f);
            return;
        }
        else if (collision.CompareTag("WallLeft") == true)
        {
            ApplyForce(Vector2.right * 3f);
            return;
        }
        else
        {
            return; // 何もしない
        }
        // 敵数カウントを減らす
        enemyGenerator.OnEnemyDestroyed();
        // 破壊する時に爆発エフェクト生成（生成したいもの、場所、回転）
        Instantiate(explosion, transform.position, transform.rotation);
        // EnemyShipを破壊
        Destroy(gameObject);
        // Collisionはぶつかった相手の情報が入っている。この場合は弾
        Destroy(collision.gameObject);
    }

    private void ApplyForce(Vector2 force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
