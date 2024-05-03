using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Animator anim;

    public int maxHealth = 100;
    public int currentHealth;

    public int damageValue = 35; //damage from the player

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
        currentHealth = Mathf.Max(currentHealth, 0);
        anim.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Invoke("Died", 2f);
        }
    }

    private void Died()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
    }
}
