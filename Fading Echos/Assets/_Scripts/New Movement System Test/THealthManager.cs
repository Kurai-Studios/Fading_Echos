using System.Collections;
using UnityEngine;

public class THealthManager : MonoBehaviour
{
    Animator anim;

    public float health = 100f;
    bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage; 
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
