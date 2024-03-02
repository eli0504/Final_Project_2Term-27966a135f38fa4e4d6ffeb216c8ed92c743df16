using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;

    public int maxHealth = 100;
    private float currentHealth;

    public float damageValue = 35f; //damage from the player


    private void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

    }

    public void TakeDamage()
    {
        // Reduce enemy health when taking damage
        currentHealth -= damageValue;
        // Make sure health is not less than zero
        currentHealth = Mathf.Max(currentHealth, 0f);
        anim.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Died();
            anim.SetBool("isDead", true);
        }
    }

    private void Died()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
    }
}
