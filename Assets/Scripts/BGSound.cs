using UnityEngine;
using System;
using System.Collections;

public class BGSound : MonoBehaviour
{
    private AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        StartCoroutine(SoundBackground());
        
    }

    IEnumerator SoundBackground()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(5f,8f));
            _audio.Play();
        }
    }


}
