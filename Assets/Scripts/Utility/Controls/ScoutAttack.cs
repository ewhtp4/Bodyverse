using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject attackPrefab;

    public float attackForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        GameObject attack = Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = attack.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * attackForce, ForceMode2D.Impulse);
    }
}
