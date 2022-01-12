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
}
