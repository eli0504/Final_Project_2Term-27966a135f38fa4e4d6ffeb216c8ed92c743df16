using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public HealthBar healthBar;

    private int maxHealth = 200;
    private int currentHealth;

    private int damageValue = 20; //damage from the player

    private Win win;

    public GameObject goldKey;

    // Attack speed
    public float normalAttackSpeed = 1f;
    public float fastAttackSpeed = 0.5f;
    public float attackSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        win = GetComponent<Win>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        attackSpeed = normalAttackSpeed; // Set initial attack speed
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
            StartCoroutine(Died());
        }
        else if (currentHealth <= maxHealth / 2) // Check if health is less than or equal to half
        {
            Color red = new Color(1f, 0.5f, 0.5f);
            spriteRenderer.color = red; // Change color to red
            attackSpeed = fastAttackSpeed; // Increase attack speed
            anim.speed = fastAttackSpeed; // Apply the faster attack speed to the animator
        }

        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Died()
    {
        anim.SetBool("isDead", true);

        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        Instantiate(goldKey, new Vector3(-1f, 2f, 0), Quaternion.identity);
    }
}
