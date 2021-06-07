using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSong : MonoBehaviour
{
    private Shoot shoot;

    [SerializeField] GameObject song;
    [SerializeField] GameObject playSong;

    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    private void Shot()
    {
        Instantiate(song, transform.position, Quaternion.identity);
        playSong.SetActive(true);
    }
}
