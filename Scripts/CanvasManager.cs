using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject songSelect;
    public GameObject playSong;
    public GameObject game;

    void Update()
    {
        if (songSelect == null)
        {
            songSelect = GameObject.Find("Song Select");
        }
        if (playSong == null)
        {
            playSong = GameObject.Find("Play Song Canvas");
        }
        if (game == null)
        {
            game = GameObject.Find("Hide at Start");
        }
    }
}
