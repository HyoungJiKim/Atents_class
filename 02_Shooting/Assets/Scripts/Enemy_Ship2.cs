using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ship2 : Enemy_Ship
{
    public GameObject energy;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            energy.SetActive(true);
            energy.GetComponent<PowerUp>().enabled = true;
            energy.GetComponent<CapsuleCollider2D>().enabled = true;
            energy.transform.parent = null;
        }
        base.OnCollisionEnter2D(collision);
    }

}
