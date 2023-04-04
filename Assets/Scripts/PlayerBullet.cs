using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    // 上に動く
    void Update()
    {
        transform.position += new Vector3(0, 3f, 0) * Time.deltaTime;

        // 弾の表示範囲
        if (transform.position.y > 3)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyShip") == true)
        {
            // 敵に攻撃を与える
            EnemyShip enemy = collision.GetComponent<EnemyShip>();
            enemy.ApplyDamage(Const.PLAYER.CONST_PLAYER_ATTACK);

            // 弾を破壊する
            Destroy(gameObject);
        }
    }
}
