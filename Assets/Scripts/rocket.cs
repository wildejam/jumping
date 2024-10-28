using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public float shotForce = 250.0f;
    public float flightTime = 5f;
    public float explosionRadius = 10f;
    public float explosionForce = 1000f;
    public float explosionUpwardForce = 5f;

    [SerializeField] private LayerMask explosionLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * shotForce);
    }

    private void Update()
    {
        // Destroy the rocket after it has existed for a certain time
        Destroy(gameObject, flightTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Rocket Collided: explosion calculating");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        Debug.Log("Detected collided objects: ");
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i].gameObject.name);
            hitColliders[i].attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardForce);
            Debug.Log("force added!");
        }

        Destroy(gameObject, 0.1f);
    }
}
