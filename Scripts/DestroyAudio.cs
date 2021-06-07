using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying != true)
        {
            Destroy(this.gameObject);
        }
    }
}
