using UnityEngine;

public class TBullet : MonoBehaviour
{
    [SerializeField] float delayDestroy;
    float timer;

    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= delayDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
