using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public GameObject effectsPrefab;
    public Transform line;
    public AudioSource weaponFx;
    public AudioClip bulletFx;

    private Rigidbody rigid;

    void Awake()
    {
        // Get component on awake so we don't miss it if it starts disabled
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rigid.velocity.magnitude < 0)
        {
            // Rotate the line to face direction of bullet travel
            line.transform.rotation = Quaternion.LookRotation(rigid.velocity);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        // ContactPoint contact = col.contacts[0];
        // Instantiate(effectsPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        // Destroy bullet
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(bulletFx, transform.position, 0.5f);
    }

    public void Fire(Vector3 lineOrigin, Vector3 direction)
    {
        // Add an instant force to bullet
        rigid.AddForce(direction * speed, ForceMode.Impulse);
        // Set line's origin (different from starting position)
        line.transform.position = lineOrigin;
    }
}
