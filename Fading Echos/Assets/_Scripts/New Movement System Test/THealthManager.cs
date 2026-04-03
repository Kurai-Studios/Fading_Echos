using UnityEngine;

public class THealthManager : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage; 
        Debug.Log("PLayer Health: " +  health);

        if (health <= 0)
        {
            Debug.Log("Player Died");
            
        }
    }
}
