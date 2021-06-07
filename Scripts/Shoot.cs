using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PlayerInput))]
public class Shoot : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] GameObject leftMuzzle;
    [SerializeField] GameObject rightMuzzle;

    [SerializeField] GameObject redFlash;
    [SerializeField] GameObject blueFlash;
    [SerializeField] GameObject redHit;
    [SerializeField] GameObject blueHit;
    [SerializeField] LineRenderer redBeam;
    [SerializeField] LineRenderer blueBeam;

    [SerializeField] LayerMask mask;

    [SerializeField] float beamTime = 0.1f;

    public AudioClip[] shootClipArray;

    public GameObject currentGun { get; private set; }
    public GameObject currentLeft;
    public GameObject currentRight;

    private bool leftHeldDown = false;
    private bool rightHeldDown = false;
    private bool leftHeld = false;
    private bool rightHeld = false;

    private Coroutine redBeamTimer;
    private Coroutine blueBeamTimer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        currentGun = leftMuzzle;

        redFlash.SetActive(false);
        blueFlash.SetActive(false);
        redHit.SetActive(false);
        blueHit.SetActive(false);
        redBeam.enabled = false;
        blueBeam.enabled = false;
    }

    void Update()
    {
        if (playerInput.leftShootInput == 1 && leftHeldDown == false)
        {
            //Debug.Log("LEFT SHOOT");

            currentGun = leftMuzzle;

            if (this.redBeamTimer != null)
            {
                StopCoroutine(this.redBeamTimer);
            }

            this.redBeamTimer = StartCoroutine(RedBeam(beamTime));

            leftHeldDown = true;

            ShootGun();
            Audio();
            Haptics();
        }
        else if (playerInput.leftShootInput == 0)
        {
            leftHeldDown = false;
        }

        if (playerInput.rightShootInput == 1 && rightHeldDown == false)
        {
            //Debug.Log("RIGHT SHOOT");

            currentGun = rightMuzzle;

            if (this.blueBeamTimer != null)
            {
                StopCoroutine(this.blueBeamTimer);
            }

            this.blueBeamTimer = StartCoroutine(BlueBeam(beamTime));

            rightHeldDown = true;

            ShootGun();
            Audio();
            Haptics();
        }
        else if (playerInput.rightShootInput == 0)
        {
            rightHeldDown = false;
        }
    }

    private void FixedUpdate()
    {
        if (leftHeld == true && playerInput.leftShootInput == 1)
        {
            RaycastHit leftHeldNote;

            if (Physics.Raycast(leftMuzzle.transform.position, leftMuzzle.transform.TransformDirection(Vector3.forward), out leftHeldNote, Mathf.Infinity))
            {
                if (leftHeldNote.transform.gameObject.layer == LayerMask.NameToLayer("Red"))
                {
                    if (this.redBeamTimer != null)
                    {
                        StopCoroutine(this.redBeamTimer);
                    }

                    this.redBeamTimer = StartCoroutine(RedBeam(beamTime));

                    currentLeft = leftHeldNote.transform.parent.gameObject;
                }
                else
                {
                    leftHeld = false;
                }
            }
            else
            {
                leftHeld = false;
            }
        }
        else
        {
            if (currentLeft != null)
            {
                currentLeft.SendMessage("Miss", null, SendMessageOptions.DontRequireReceiver);
            }
        }

        if (rightHeld == true && playerInput.rightShootInput == 1)
        {
            RaycastHit rightHeldNote;

            if (Physics.Raycast(rightMuzzle.transform.position, rightMuzzle.transform.TransformDirection(Vector3.forward), out rightHeldNote, Mathf.Infinity))
            {
                if (rightHeldNote.transform.gameObject.layer == LayerMask.NameToLayer("Blue"))
                {
                    if (this.blueBeamTimer != null)
                    {
                        StopCoroutine(this.blueBeamTimer);
                    }

                    this.blueBeamTimer = StartCoroutine(BlueBeam(beamTime));

                    currentRight = rightHeldNote.transform.parent.gameObject;
                }
                else
                {
                    rightHeld = false;
                }
            }
            else
            {
                rightHeld = false;
            }
        }
        else
        {
            if (currentRight != null)
            {
                currentRight.SendMessage("Miss", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void ShootGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(currentGun.transform.position, currentGun.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            //Debug.DrawRay(currentGun.transform.position, currentGun.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");

            if (hit.transform.parent == null)
            {
                hit.transform.SendMessage("Shot", null, SendMessageOptions.DontRequireReceiver);
            }
            else if (hit.transform.parent != null && hit.transform.parent.CompareTag("Note"))
            {
                hit.transform.parent.SendMessage("Shot", null, SendMessageOptions.DontRequireReceiver);

                if (hit.transform.parent.gameObject.layer == LayerMask.NameToLayer("Red") && currentGun == leftMuzzle)
                {
                    leftHeld = true;
                }
                else if (hit.transform.parent.gameObject.layer == LayerMask.NameToLayer("Blue") && currentGun == rightMuzzle)
                {
                    rightHeld = true;
                }
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                hit.transform.SendMessage("Shot", null, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void Audio()
    {
        AudioSource.PlayClipAtPoint(shootClipArray[Random.Range(0, shootClipArray.Length)], currentGun.transform.position, 0.3f);
    }

    IEnumerator RedBeam(float time)
    {
        var instruction = new WaitForEndOfFrame();

        redFlash.SetActive(true);
        redHit.SetActive(true);
        redBeam.enabled = true;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return instruction;
        }

        redFlash.SetActive(false);
        redHit.SetActive(false);
        redBeam.enabled = false;
    }

    IEnumerator BlueBeam(float time)
    {
        var instruction = new WaitForEndOfFrame();

        blueFlash.SetActive(true);
        blueHit.SetActive(true);
        blueBeam.enabled = true;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return instruction;
        }

        blueFlash.SetActive(false);
        blueHit.SetActive(false);
        blueBeam.enabled = false;
    }

    private void Haptics()
    {
        //OpenXR currently does not support haptics
        //playerInput.leftController.GetComponent<ActionBasedController>().SendHapticImpulse(5, 10);
    }
}
