using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerShipを方向キーで動かす
// ・方向キーの入力取得
// ・Playerの位置取得

// 弾をうつ
// ・弾を作る
// ・弾の動きを作る
// ・発射ポイントを作る
// ・ボタンを押した時に弾を生成する Instantiate
public class PlayerShip : MonoBehaviour
{
    public Transform firePoint; // 弾を発射する位置
    public GameObject bulletPrefab;
    public GameObject explosion; // 爆発のプレファブ

    AudioSource audioSource;
    public AudioClip shotSE;
    GameController gameController;

    private void Start()
    {
        // 同じオブジェクト内の"AudioSource"というコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>(); ;
    }

    // 約0.2秒に一回実行される関数
    void Update()
    {
        Shot();
        Move();
    }

    void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            audioSource.PlayOneShot(shotSE);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") == true)
        {
            // 爆発エフェクト
            Instantiate(explosion, transform.position, transform.rotation);
            // PlayerShipを破壊
            Destroy(gameObject);
            // Collisionはぶつかった相手の情報が入っている。この場合は弾
            Destroy(collision.gameObject);
            gameController.GameOver();
        }
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        // Playerの移動範囲制御（左右のみ）
        Vector3 nextPosition = transform.position + new Vector3(x, 0, 0) * Time.deltaTime * 4f;
        nextPosition = new Vector3(
            Mathf.Clamp(nextPosition.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPosition.y, -2f, 2f),
            nextPosition.z
            );
        transform.position = nextPosition;

    }
}
