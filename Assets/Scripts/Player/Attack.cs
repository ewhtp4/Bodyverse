using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject attackPrefab;
    private AudioSource AttackSound;

    public float attackForce = 20f;

    void Start()
    {
        AttackSound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AttackSound.Play();
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject attack = Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * attackForce, ForceMode2D.Impulse);
    }
}
