using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyShip : MonoBehaviour
{
    // 8方向に弾をうつ
    // ・1方向に弾をうつ
    // ・好きな角度に弾をうつ
    // ・360°を8分割して、弾を発射する
    // 一定間隔で実行する
    // ・コルーチン：時間の制御が楽になるもの
    // 敵の行動を管理する
    // ・コルーチン
    // 特定の位置まで移動
    // ・Bossの生成
    // ・特定の位置まで移動
    // ・移動してから攻撃する
    // 弾幕の実装
    // ・幾何学模様
    // ・Playerを狙う
    GameObject player;
    public BossEnemyBullet bulletPrefab;
    public GameObject explosion; // 爆発のプレファブ
    GameController gameController;
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        StartCoroutine(CPU());
    }

    void Shot(float angle, float speed)
    {
        BossEnemyBullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Setting(angle, speed); // Mathf.PI/4fは45°
    }

    IEnumerator CPU()
    {
        // 特定より上だったら
        while (transform.position.y > 1f)
        {
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
            yield return null; // 1フレーム(0.02秒)待つ
        }

        while (true)
        {
            // n回m方向に撃ちたい
            yield return WaveNShotM(4, 8);
            yield return new WaitForSeconds(1f);
            yield return WaveNShotMCurve(4, 16);
            yield return new WaitForSeconds(1f);
            yield return WaveNPlayerAimShot(4, 6);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator WaveNShotM(int n, int m)
    {
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(0.3f);
            yield return ShotNCurve(m, 2);
        }
    }

    IEnumerator WaveNShotMCurve(int n, int m)
    {
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(0.3f);
            yield return ShotNCurve(m, 2);
        }
    }

    IEnumerator WaveNPlayerAimShot(int n, int m)
    {
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(1f);
            PlayerAimShot(m, 3);
        }
    }

    void ShotN(int count, float speed)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = i * (2 * Mathf.PI / count); // 2PI：360
            Shot(angle, speed);
        }
    }

    IEnumerator ShotNCurve(int bulletCount, float speed)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (2 * Mathf.PI / bulletCount); // 2PI：360
            Shot(angle - Mathf.PI / 2f, speed);
            Shot(-angle - Mathf.PI / 2f, speed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Playerを狙う
    // ・Playerの位置取得
    // ・どの角度に撃てばいいかを計算
    void PlayerAimShot(int bulletCount, float speed)
    {
        // 自分からみたPlayerの位置を計算する
        Vector3 diffPosition = player.transform.position - transform.position;
        // 自分からみたPlayerの角度を出す：傾きから角度を出す：アークタンジェントを使う
        float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = (i - bulletCount / 2f) * ((Mathf.PI / 2f) / bulletCount); // 2PI：360
            Shot(angleP + angle, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collisionにぶつかった相手の情報が入っている：bullet, Player
        if (collision.CompareTag("Player") == true)
        {
            return;
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
        else if (collision.CompareTag("BossEnemyShip") == true)
        {
            return; // 何もしない
        }
        // 破壊する時に爆発エフェクト生成（生成したいもの、場所、回転）
        Instantiate(explosion, transform.position, transform.rotation);
        // EnemyShipを破壊
        Destroy(gameObject);
        // Collisionはぶつかった相手の情報が入っている。この場合は弾
        Destroy(collision.gameObject);
    }
}
