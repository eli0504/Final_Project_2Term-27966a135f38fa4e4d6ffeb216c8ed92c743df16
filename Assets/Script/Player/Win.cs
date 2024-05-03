using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Win : MonoBehaviour
{
    public ParticleSystem winParticles;
    public ParticleSystem winParticles2;

    public GameObject winPanel;

    private void Start()
    {
        winPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "goldKey")
        {
            Destroy(other.gameObject);
            audioLibrary.PlaySound("live");
            winParticles.Play();
            winParticles2.Play();
            StartCoroutine(waitForWinPanel());
        }
    }

    IEnumerator waitForWinPanel()
    {
        yield return new WaitForSeconds(5);

        winPanel.SetActive(true);
    }
}
