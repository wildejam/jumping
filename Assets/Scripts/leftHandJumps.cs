using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class leftHandJumps : MonoBehaviour
{
    public InputActionReference trigger;
    public InputActionReference button;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject jumpTarget;
    private bool trajJumpEnabled = false;
    private bool isGrounded = false;

    public float jumpPower = 500f;
    public float trajJumpPower = 100f;
    public float maxTrajJumpDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        trigger.action.performed += TrajJump;
        button.action.performed += ButtonJump;
        jumpTarget.SetActive(false);
    }
    private void Update()
    {
        // Sets the trajJump visual element if it is active
        if (jumpTarget.activeSelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxTrajJumpDistance))
            {
                jumpTarget.transform.position = hit.point;
            }
            else
            {
                jumpTarget.transform.position = transform.position + (transform.forward * maxTrajJumpDistance);
            }
            
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, -transform.up, out hit, 1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void ButtonJump(InputAction.CallbackContext __)
    {
        if (isGrounded)
        {
            player.GetComponent<Rigidbody>().AddForce(transform.up * jumpPower);
        }
    }
    void TrajJump(InputAction.CallbackContext __)
    {
        // Switch traj jump status. Perform the jump && disable target visual if it was disabled. Enable target visual if enabled.
        trajJumpEnabled = !trajJumpEnabled;

        if (!trajJumpEnabled) { initiateTrajJump(); jumpTarget.SetActive(false); }
        else { jumpTarget.SetActive(true); }
    }
    void initiateTrajJump()
    {
        if (!isGrounded) { return; }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxTrajJumpDistance))
        {
            Debug.Log("Jumping to floor position!");
            player.GetComponent<Rigidbody>().AddForce(player.transform.up * jumpPower);
            player.GetComponent<Rigidbody>().AddForce((jumpTarget.transform.position - transform.position) * trajJumpPower);
        }
        else
        {
            Debug.Log("Jumping to floating position!");
            player.GetComponent<Rigidbody>().AddForce(player.transform.up * jumpPower);
            player.GetComponent<Rigidbody>().AddForce((jumpTarget.transform.position - transform.position) * trajJumpPower);
        }
    }


}
