using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class THealthManager : MonoBehaviour
{
    Animator anim;

    public float health = 100f;
    bool isDead = false;
    public Image healthBar;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        healthBar.fillAmount = health / 100;
        Debug.Log("PLayer Health: " +  health);

        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        isDead = true;
        anim.SetTrigger("Death");
        GetComponent<TMovementManager>().isDead = true;
    }
}
