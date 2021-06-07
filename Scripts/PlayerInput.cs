using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;
    
    public float leftShootInput { get; private set; }
    public float rightShootInput { get; private set; }
    public float pauseInput { get; private set; }

    private void Update()
    {
        leftShootInput = leftController.GetComponent<ActionBasedController>().activateAction.action.ReadValue<float>();
        rightShootInput = rightController.GetComponent<ActionBasedController>().activateAction.action.ReadValue<float>();

    }
}
