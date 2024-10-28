using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHandJumps : MonoBehaviour
{
    public InputActionReference trigger;

    [SerializeField] private GameObject rocketTemplate;
    [SerializeField] private GameObject rightHand;


    // Start is called before the first frame update
    void Start()
    {
        trigger.action.performed += FireRocket;
    }

    private void FireRocket(InputAction.CallbackContext __)
    {
        Debug.Log("Rocket fired!");
        Instantiate(rocketTemplate, rightHand.transform.position, rightHand.transform.rotation);
    }
}
