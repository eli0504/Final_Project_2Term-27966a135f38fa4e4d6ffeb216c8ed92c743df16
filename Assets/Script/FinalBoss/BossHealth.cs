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

    public GameObject boss;

    private Win win;

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
            win.PlayerWin();
        }

        healthBar.SetHealth(currentHealth);
    }

    private void Died()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject);
    }
}
