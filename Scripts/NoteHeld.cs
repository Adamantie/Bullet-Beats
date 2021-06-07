using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHeld : MonoBehaviour
{
    private Shoot shoot;
    private Score score;
    private Streak streak;
    private GameObject muzzleColor;
    private Material[] heldMaterials;
    private Material[] emptyMaterials;
    private Material dissolveBase;
    private Material dissolveGlow;
    private Vector3 stoppedPosition;
    private float startingDistance;
    private float currentDistance;
    private bool stopMovement = false;
    public bool activated = false;
    public bool dissolveActivated = false;

    public Color dissolveColor;

    [SerializeField] GameObject noteHeld;
    [SerializeField] GameObject noteEmpty;
    [SerializeField] GameObject[] hitClipArray;
    [SerializeField] GameObject[] missClipArray;
    [SerializeField] Material transparentBase;
    [SerializeField] Material transparentGlow;

    private void Awake()
    {
        shoot = FindObjectOfType<Shoot>();
        score = FindObjectOfType<Score>();
        streak = FindObjectOfType<Streak>();
        heldMaterials = noteHeld.GetComponent<Renderer>().materials;
        emptyMaterials = noteEmpty.GetComponent<Renderer>().materials;
        dissolveColor = Color.white;

        dissolveBase = heldMaterials[0];
        dissolveGlow = heldMaterials[1];

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
        if (stopMovement == true)
        {
            currentDistance = Vector3.Distance(noteEmpty.transform.position, noteHeld.transform.position);
            heldMaterials[2].Lerp(heldMaterials[1], heldMaterials[0], currentDistance / startingDistance);
            noteHeld.GetComponent<Renderer>().materials = heldMaterials;

            GetComponent<Dypsloom.RhythmTimeline.Core.Notes.HeldNotePositions>().stopped = true;
            GetComponent<Dypsloom.RhythmTimeline.Core.Notes.HeldNotePositions>().m_StartNote.position = stoppedPosition;

            //Debug.Log(currentDistance);
        }

        if (gameObject.transform.GetChild(1).position.z <= gameObject.transform.GetChild(0).position.z || (gameObject.transform.GetChild(1).position.z < 16 && gameObject.transform.GetChild(1).position.z > 15))
        {
            //Debug.Log("OOF");

            if (noteHeld.GetComponent<BoxCollider>().enabled != false)
            {
                streak.IncreaseStreak();
            }

            if (gameObject.layer == LayerMask.NameToLayer("Red"))
            {
                shoot.currentLeft = null;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Blue"))
            {
                shoot.currentRight = null;
            }

            gameObject.transform.GetChild(1).position = stoppedPosition;
            noteHeld.GetComponent<BoxCollider>().enabled = false;
            noteEmpty.GetComponent<BoxCollider>().enabled = false;
            noteHeld.transform.GetChild(0).gameObject.SetActive(false);
            noteEmpty.transform.GetChild(0).gameObject.SetActive(false);
            Dissolve();
        }

        if (gameObject.transform.GetChild(1).position.z >= 40 && noteEmpty.GetComponent<BoxCollider>().enabled == false)
        {
            //Debug.Log("Reset");
            dissolveColor = Color.white;
            heldMaterials[0].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[1].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[2].SetColor("_EdgeColor", dissolveColor);
            emptyMaterials[0].SetColor("_EdgeColor", dissolveColor);

            noteHeld.GetComponent<BoxCollider>().enabled = true;
            noteEmpty.GetComponent<BoxCollider>().enabled = true;
            noteHeld.transform.GetChild(0).gameObject.SetActive(true);
            noteEmpty.transform.GetChild(0).gameObject.SetActive(true);
            activated = false;
            dissolveActivated = false;
            GetComponent<Dypsloom.RhythmTimeline.Core.Notes.HeldNotePositions>().stopped = false;
        }

        if (currentDistance != 0 && currentDistance != startingDistance && noteHeld.GetComponent<BoxCollider>().enabled == true)
        {
            score.HeldScore();
        }
    }

    private void Shot()
    {
        if (shoot.currentGun == muzzleColor)
        {
            score.UpdateScore(dissolveColor);
            streak.IncreaseStreak();
            //Debug.Log("Hit");
            startingDistance = Vector3.Distance(noteHeld.transform.position, noteEmpty.transform.position);
            //stop movement
            stoppedPosition = gameObject.transform.GetChild(0).position;
            stopMovement = true;
            //play hit audio
            Instantiate(hitClipArray[Random.Range(0, hitClipArray.Length)], transform.position, Quaternion.identity);
        }
        else
        {
            //Debug.Log("Wrong Color");

            Miss();
        }
    }

    private void Dissolve()
    {
        if (dissolveActivated == false)
        {
            score.UpdateScore(dissolveColor);

            stopMovement = false;
            currentDistance = startingDistance;

            //make transparent
            heldMaterials[0] = dissolveBase;
            heldMaterials[1] = dissolveGlow;
            emptyMaterials[0] = dissolveBase;

            heldMaterials[2].Lerp(heldMaterials[1], heldMaterials[0], currentDistance / startingDistance);

            noteHeld.GetComponent<Renderer>().materials = heldMaterials;
            noteEmpty.GetComponent<Renderer>().materials = emptyMaterials;
            //spawn hit particles
            heldMaterials[0].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[1].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[2].SetColor("_EdgeColor", dissolveColor);
            emptyMaterials[0].SetColor("_EdgeColor", dissolveColor);

            //Debug.Log("dissolve");

            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dissolve");
            gameObject.transform.GetChild(1).GetComponent<Animator>().SetTrigger("Dissolve");

            dissolveActivated = true;
        }
    }

    private void Miss()
    {
        if (activated == false && noteHeld.GetComponent<BoxCollider>().enabled == true)
        {
            streak.ResetStreak();
            dissolveColor = Color.white;
            heldMaterials[0].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[1].SetColor("_EdgeColor", dissolveColor);
            heldMaterials[2].SetColor("_EdgeColor", dissolveColor);
            emptyMaterials[0].SetColor("_EdgeColor", dissolveColor);

            startingDistance = Vector3.Distance(noteHeld.transform.position, noteEmpty.transform.position);
            //stop movement
            stoppedPosition = gameObject.transform.GetChild(0).position;
            stopMovement = true;
            //play miss audio
            Instantiate(missClipArray[Random.Range(0, missClipArray.Length)], transform.position, Quaternion.identity);
            //make transparent
            heldMaterials[0] = transparentBase;
            heldMaterials[1] = transparentGlow;
            emptyMaterials[0] = transparentBase;

            noteHeld.GetComponent<Renderer>().materials = heldMaterials;
            noteEmpty.GetComponent<Renderer>().materials = emptyMaterials;
            //deactivate hitbox
            noteHeld.GetComponent<BoxCollider>().enabled = false;

            activated = true;
        }
    }

    private void Perfect()
    {
        //Debug.Log("Perfect Held");
        dissolveColor = Color.yellow;
    }

    private void Great()
    {
        //Debug.Log("Great Held");
        dissolveColor = Color.magenta;
    }
}
