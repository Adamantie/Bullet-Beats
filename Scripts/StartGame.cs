using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Shoot shoot;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject songSelect;

    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
    }

    private void Shot()
    {
        menu.SetActive(false);
        songSelect.SetActive(true);
    }
}
