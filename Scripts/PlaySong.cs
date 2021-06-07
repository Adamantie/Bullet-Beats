using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySong : MonoBehaviour
{
    private Shoot shoot;

    [SerializeField] GameObject playSongMenu;
    [SerializeField] GameObject playSong;
    [SerializeField] GameObject game;

    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    private void Shot()
    {
        Destroy(GameObject.Find("Inova - Desert Clip(Clone)"));

        playSongMenu.SetActive(false);
        playSong.SetActive(false);
        game.SetActive(true);
    }
}
