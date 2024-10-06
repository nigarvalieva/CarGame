using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSoundObj : MonoBehaviour
{
    public GameObject SoundController;
    private static bool IsCreated = false;

    private void Start()
    {
        if (IsCreated) return;

        IsCreated = true;
        DontDestroyOnLoad(Instantiate(SoundController));
    }

}
