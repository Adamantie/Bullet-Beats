using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHorizontal : MonoBehaviour
{
    private Shoot shoot;
    private Score score;
    private Streak streak;
    private GameObject muzzleColor;
    private Material[] materials;

    public Color dissolveColor;

    [SerializeField] GameObject[] hitClipArray;
    [SerializeField] GameObject[] missClipArray;

    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
        score = FindObjectOfType<Score>();
        streak = FindObjectOfType<Streak>();
        materials = GetComponent<Renderer>().materials;
        dissolveColor = Color.white;

        if (gameObject.layer == LayerMask.NameToLayer("Red"))
        {
            muzzleColor = GameObject.Find("Left Muzzle");
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Blue"))
        {
            muzzleColor = GameObject.Find("Right Muzzle");
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.z >= 40 && GetComponent<BoxCollider>().enabled == false)
        {
            //Debug.Log("Reset");
            dissolveColor = Color.white;
            materials[0].SetColor("_EdgeColor", dissolveColor);
            materials[1].SetColor("_EdgeColor", dissolveColor);
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void Shot()
    {
        if (shoot.currentGun == muzzleColor && ((shoot.currentGun.transform.parent.parent.localRotation.eulerAngles.z > 45 && shoot.currentGun.transform.parent.parent.localRotation.eulerAngles.z < 135) || (shoot.currentGun.transform.parent.parent.localRotation.eulerAngles.z < 315 && shoot.currentGun.transform.parent.parent.localRotation.eulerAngles.z > 225)))
        {
            //Debug.Log("Hit");
            score.UpdateScore(dissolveColor);
            streak.IncreaseStreak();
            //play hit audio
            Instantiate(hitClipArray[Random.Range(0, hitClipArray.Length)], transform.position, Quaternion.identity);
            //spawn hit particles
            materials[0].SetColor("_EdgeColor", dissolveColor);
            materials[1].SetColor("_EdgeColor", dissolveColor);
            gameObject.GetComponent<Animator>().SetTrigger("Dissolve");
            //destroy gameObject
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            Miss();
        }
    }

    private void Miss()
    {
        streak.ResetStreak();
        //Debug.Log("Wrong Orientation " + shoot.currentGun.transform.parent.parent.localRotation.eulerAngles.z);
        dissolveColor = Color.white;
        materials[0].SetColor("_EdgeColor", dissolveColor);
        materials[1].SetColor("_EdgeColor", dissolveColor);
        //play miss audio
        Instantiate(missClipArray[Random.Range(0, missClipArray.Length)], transform.position, Quaternion.identity);
        //spawn miss particles
        gameObject.GetComponent<Animator>().SetTrigger("Dissolve");
        //destroy gameObject
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Perfect()
    {
        //Debug.Log("Perfect Horizontal");
        dissolveColor = Color.yellow;
    }

    private void Great()
    {
        //Debug.Log("Great Horizontal");
        dissolveColor = Color.magenta;
    }
}
