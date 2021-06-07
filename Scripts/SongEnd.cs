using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongEnd : MonoBehaviour
{
    private CanvasManager canvasManager;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void Update()
    {
        if (gameObject.transform.position.z <= 15)
        {
            canvasManager.songSelect.SetActive(true);
            canvasManager.playSong.SetActive(true);
            canvasManager.game.SetActive(false);
        }
    }
}
