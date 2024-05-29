using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

public class OnTrigger : MonoBehaviour
{
    private Loader loader;
    private Health healthScript;
    public LevelPlayerpref levelPlayerpref;
    private PlayerController playerScript;

    public GameObject player;
    public GameObject bigPotion;
    public GameObject smallPotion;
    public GameObject heart;
    public GameObject goldKey;
    public GameObject teleport;
    public GameObject dashPowerUp;
    
    public GameObject rememberPanel;

    public int index;

    private Rigidbody2D rb;

    //COINS
    public TextMeshProUGUI coinsCounterText;
    public int coinsNeeded = 27;
    public int goldenCoinsNeeded = 18;
    public int coinsCollected = 0;
    private int coinsCounter = 0;

    //velocity power up
    public GameObject velocityPowerUp;
    private float originalSpeed; // Velocidad original del jugador
    public float speedBoost = 20f; // Factor de aumento de velocidad
    private float powerUpDuration = 20f; // Duración del Power-up
    public TextMeshProUGUI countdownText;

    public Volume volume;
    private Vignette vignette;

    public ParticleSystem boxParticles;
    public ParticleSystem boxParticles2;
    public ParticleSystem dashParticles;

    private float smallPowerUp = 0.3f;
    private float bigPowerUp = 1f;
   
    private void Awake()
    {
        QuitRememberPanel();

        volume = GetComponent<Volume>();
    }
    private void Start()
    {
        playerScript = GetComponent<PlayerController>();

        originalSpeed = playerScript.moveSpeed;

        volume.profile.TryGet(out vignette); //find and plug the vignette

        vignette.intensity.value = 0.5f;
        vignette.color.value = Color.red;
        
        healthScript = GetComponent<Health>();

        loader = FindObjectOfType<Loader>();
        if (loader == null)
        {
            Debug.LogError("Loader not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //going to secret level
        if (other.gameObject.tag == "crystal")
        {
            if (levelPlayerpref != null)
            {
                levelPlayerpref.EnterHiddenLevel(); 
            }
        }
        if (other.gameObject.tag == "ExitHiddenLevel")
        {
            if (levelPlayerpref != null)
            {
                levelPlayerpref.ExitHiddenLevel();
            }
        }

        //coins
        if (other.gameObject.tag == "coins")
        {
            coinsCollected++;
            coinsCounter++;
            audioLibrary.PlaySound("coin");
            Destroy(other.gameObject); // Hace que el objeto desaparezca
            coinsCounterText.text = $"{coinsCounter}";
        }

        if (other.gameObject.tag == "GoldenCoin")
        {
            coinsCollected++;
            coinsCounter++;
            audioLibrary.PlaySound("coin");
            Destroy(other.gameObject); // Hace que el objeto desaparezca
            coinsCounterText.text = $"{coinsCounter}";

            if (coinsCollected == goldenCoinsNeeded)
            {
                Instantiate(velocityPowerUp, new Vector3(-5, -8, 0), Quaternion.identity);
            }
        }

        //PowerUp
        if (other.gameObject.tag == "smallPowerUp")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("poison");
            transform.localScale = new Vector3(smallPowerUp, smallPowerUp, 0);
        }
        else if (other.gameObject.tag == "bigPowerUp")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("poison");
            transform.localScale = new Vector3(bigPowerUp, bigPowerUp, 0);
        }

        if (other.gameObject.tag == "velocityPowerUp")
        {
            ActivatePowerUp();
            Destroy(other.gameObject);
        }

        //enemies and traps
        if (other.gameObject.tag == "traps" || other.gameObject.tag == "enemy")
        {
            healthScript = GetComponent<Health>();

            if (healthScript != null)
            {
                StartCoroutine(Desactive()); //The red vignette appears when the player takes damage.
                healthScript.GetDamage();
            }
        }

        //box
        if (other.CompareTag("box"))
        {
            Destroy(other.gameObject);
            Instantiate(smallPotion, new Vector3(-6, -2, 0), Quaternion.identity);
            boxParticles.Stop();
        }
        else if (other.CompareTag("box2"))
        {
            Destroy(other.gameObject);
            Instantiate(bigPotion, new Vector3(44.85f, -1.55f, 0), Quaternion.identity);
            boxParticles2.Stop();
        }

        //dash chest
        if (other.CompareTag("dash"))
        {
            Instantiate(dashPowerUp, new Vector3(1.7f, 0.19f, 0), Quaternion.identity);
            dashParticles.Stop();
            Destroy(other.gameObject);
        }
      
        //live
        if (other.CompareTag("live"))
        {
            audioLibrary.PlaySound("live");
            Health.lives++;
            Destroy(other.gameObject);
        }

        //edges
        if (other.CompareTag("Edge"))
        {
            transform.position = new Vector3(-15f, -2.78f, 1f);
        } else if (other.CompareTag("Edge2"))
        {
            transform.position = new Vector3(152.7f, 5.23f, 1f);
        } else if (other.CompareTag("Edge3"))
        {
            transform.position = new Vector3(-15f, -2.78f, 1f);
            healthScript.GetDamage();
        }

        //teleport
        if (other.CompareTag("teleport"))
        {
            transform.position = new Vector3(-31.75f, 0.1f, 0f);
        }

        //every time the player jumps into the void he will lose a life

        if (other.CompareTag("respawn"))
        {
            healthScript.GetDamage();
        }

       //ChangeLevel
        if (coinsCollected >= coinsNeeded && other.CompareTag("PassLevel"))
        {
            int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
            {
                loader.LoadScene(nextSceneIndex); 
            }
        }
        else if (other.CompareTag("PassLevel"))
        {
           ShowRememberPanel();
        }

       //bullet
        if (other.CompareTag("bullet"))
        {
            healthScript.GetDamage();
        }
    }

    //velocity power up
    private void ActivatePowerUp()
    {
        StartCoroutine(PowerUpRoutine());
    }

    private IEnumerator PowerUpRoutine()
    {
        Debug.Log("Power-up activado");
        playerScript.moveSpeed *= speedBoost;

        float countdown = powerUpDuration;
        while (countdown > 0)
        {
            countdownText.text = $"PowerUp:{countdown:F0}";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        playerScript.moveSpeed = originalSpeed;
        countdownText.text = "Power-up terminado";
        Debug.Log("Power-up desactivado");
    }

    //to remember the player to collect all the coins
    public void ShowRememberPanel()
    {

        rememberPanel.SetActive(true);

    }

    public void QuitRememberPanel()
    {
        rememberPanel.SetActive(false);
    }

    //coroutine to change color and disable vignette
    public IEnumerator Desactive() 
    {
        yield return new WaitForSeconds(0.1f);
        vignette.intensity.value = 1f;
        vignette.color.value = Color.red;

        yield return new WaitForSeconds(0.5f);
        vignette.active = false;
    }

}
       
