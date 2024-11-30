using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f; // Can
    public float speed = 3f; // Takip hızı
    private Transform player; // Oyuncunun konumu

    private void Start()
    {
        // Oyuncuyu bul
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        // Oyuncuya doğru hareket et
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Düşmanı yok et
        }
    }

}
