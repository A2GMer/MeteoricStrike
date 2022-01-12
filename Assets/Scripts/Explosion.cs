using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 音
// ・スピーカー：AudioSource
// ・音の素材：AudioClip
public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip boomSE;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(boomSE);
        Destroy(gameObject, 0.5f);
    }
}
