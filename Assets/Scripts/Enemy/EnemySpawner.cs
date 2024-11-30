using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Atanacak düşman prefab
    public float spawnInterval = 2f; // Kaç saniyede bir düşman oluşturulacak
    public float spawnAreaWidth = 5f; // Ekran genişliği (uygun değeri Unity’den alın)
    public float spawnY = 6f; // Oluşum yeri (ekranın üstü için Y koordinatı)

    private void Start()
    {
        spawnAreaWidth = Camera.main.orthographicSize * Camera.main.aspect;
        // Düşmanların belirli aralıklarla oluşturulması
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Rastgele X pozisyonu belirle
        float randomX = Random.Range(-spawnAreaWidth, spawnAreaWidth);

        // Oluşum pozisyonu belirle
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // Düşman oluştur
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
