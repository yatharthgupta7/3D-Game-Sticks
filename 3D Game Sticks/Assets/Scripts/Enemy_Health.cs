using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider healthBar;

    public float health;
    private float timeToActivateHealthbar = 2f;
    private float TimetoDeactivate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
       healthBarUI.SetActive(false);
        healthBar.value = calculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = calculateHealth();

        if(Time.time >= TimetoDeactivate)
        {
            healthBarUI.SetActive(false);
        }
        
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }

    public void damage(int Damage)
    {
        healthBarUI.SetActive(true);
        TimetoDeactivate = Time.time + timeToActivateHealthbar;
        health -= Damage;
    }

    float calculateHealth()
    {
        return health / maxHealth;
    }
}
