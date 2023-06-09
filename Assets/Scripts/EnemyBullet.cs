using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // 下方向に発射される
    void Update()
    {
        transform.position += new Vector3(0, -3, 0)*Time.deltaTime;

        // 弾の表示範囲
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}
