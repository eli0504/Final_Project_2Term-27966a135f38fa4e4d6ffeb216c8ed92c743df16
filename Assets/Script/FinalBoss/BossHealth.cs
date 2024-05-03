using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator anim;

    public HealthBar healthBar;

    public int maxHealth = 300;
    public int currentHealth;

    public int damageValue = 20; //damage from the player

    private Win win;

    public GameObject goldKey;

    private void Start()
    {
        anim = GetComponent<Animator>();
        win = GetComponent<Win>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void BossDamage()
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

        healthBar.SetHealth(currentHealth);
    }

    private void Died()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
        Instantiate(goldKey, new Vector3(-1f, 2f, 0), Quaternion.identity);
    }
}
