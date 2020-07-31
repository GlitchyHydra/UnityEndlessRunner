using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public Rigidbody m_RigidBody;
    public float m_ForwardForce;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_ForwardForce = Random.Range(200f, 300f);
        m_RigidBody.AddForce(gameObject.transform.forward * m_ForwardForce * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        m_RigidBody.constraints |= RigidbodyConstraints.FreezePositionX |
                                   RigidbodyConstraints.FreezePositionZ;
    }
}
