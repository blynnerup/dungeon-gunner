using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public float damageInvincLength = 1f;
    private float invincCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            if (invincCount <= 0)
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
        }
    }

    public void DamagePlayer()
    {        
        if(invincCount <= 0)
        {
            currentHealth--;
            invincCount = damageInvincLength;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.5f);

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
            }
        }

    }

    public void MakeInvicible(float length)
    {
        invincCount = length;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 0.5f);
    }
}
