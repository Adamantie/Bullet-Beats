using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] LayerMask mask;

    private void Awake()
    {
        reticle.SetActive(false);
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            reticle.transform.position = hit.point + new Vector3(0, 0, -0.2f);
            reticle.transform.localEulerAngles = new Vector3(0, 0, 0);
            reticle.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            reticle.SetActive(true);
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~8))
        {
            reticle.transform.position = hit.point;
            reticle.transform.localEulerAngles = new Vector3(90, 0, 0);
            reticle.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            reticle.SetActive(true);
        }
        else
        {
            reticle.SetActive(false);
        }
    }
}
