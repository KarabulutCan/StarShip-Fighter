using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f; // Merminin verdiği hasar

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Düşmana hasar ver
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Mermiyi yok et
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Mermi ekran dışına çıkarsa yok et
        Destroy(gameObject);
    }
}
