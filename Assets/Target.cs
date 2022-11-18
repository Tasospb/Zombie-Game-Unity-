using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    public int scoreAddAmount = 10;
    public Spawner spawn;

    private void Start()
    {
        spawn = FindObjectOfType<Spawner>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        GameManager.Instance.SendMessage("AddCash", null, SendMessageOptions.DontRequireReceiver);
        if (spawn.enemiesKilled >= spawn.enemySpawnAmount)
        {
            spawn.NextWave();
        }
        else
        {
            spawn.enemiesKilled++;
        }
        //GameManager.Instance.AddCash(10);
        Destroy(gameObject); 
    }
}
