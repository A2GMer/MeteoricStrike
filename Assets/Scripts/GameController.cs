using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// スコアの実装
// ・UIの作成
// ・UIの更新
// ・敵と弾がぶつかった時にスコア加算＋更新
// Playerと弾の差別化：Tag
// ゲームオーバーの実装
// ・UIの作成
// ・敵とPlayerがぶつかった時にUIを表示
// ・リトライの実装
//   ・Spaceを押したらシーン再読み込み
public class GameController : MonoBehaviour
{
    public GameObject gameOverText;

    public Text scoreText;
    int score = 1000;
    void Start()
    {
        gameOverText.SetActive(false);
        scoreText.text = "SCORE:10000";
    }

    private void Update()
    {
        if (gameOverText.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    // スコア加算
    public void AddScore()
    {
        score += 100;
        scoreText.text = "SCORE:" + score;
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
    }
}
