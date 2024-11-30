using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem; // Bağlanacak Particle System
    public Transform playerTransform;    // Player GameObject'inin Transform'u
    public float speedMultiplier = 1.0f; // Kuvveti artırmak için çarpan
    public float idleEmissionRate = 1.0f; // Dururken çıkan belli belirsiz duman oranı
    public Color idleColor = new Color(1, 1, 1, 0.1f); // Dururken kullanılan şeffaf renk
    public Vector3 offset = new Vector3(0, -0.5f, 0); // Partikül sisteminin arabaya göre konumu

    private Vector3 previousPosition;
    private float playerSpeed;

    void Start()
    {
        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem bağlanmamış!");
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player Transform bağlanmamış!");
        }

        // Başlangıç pozisyonunu kaydet
        previousPosition = playerTransform.position;

        // Particle sistemin dururken belli belirsiz çalışması için ayar
        var main = particleSystem.main;
        main.startColor = idleColor; // Varsayılan olarak şeffaf bir renk ayarla

        // Force Over Lifetime modülünü aktif et
        var forceOverLifetime = particleSystem.forceOverLifetime;
        forceOverLifetime.enabled = true;
    }

    void Update()
    {
        // Particle sistemi oyuncunun pozisyonuna göre yerleştir
        particleSystem.transform.position = playerTransform.position + offset;

        // Player'ın hızını ve yönünü hesapla
        Vector3 direction = playerTransform.position - previousPosition;
        playerSpeed = direction.magnitude / Time.deltaTime;

        var emission = particleSystem.emission;
        var main = particleSystem.main;
        var forceOverLifetime = particleSystem.forceOverLifetime;

        if (playerSpeed > 0.1f) // Eğer player hareket ediyorsa
        {
            emission.rateOverTime = playerSpeed * speedMultiplier;

            // Hareket yönüne göre kuvvet uygula (ters yönde)
            Vector3 force = direction.normalized * -playerSpeed * speedMultiplier;
            forceOverLifetime.x = new ParticleSystem.MinMaxCurve(force.x);
            forceOverLifetime.y = new ParticleSystem.MinMaxCurve(force.y);

            // Renk ayarını güncelle
            main.startColor = Color.white; // Tam parlak beyaz
        }
        else // Eğer player duruyorsa
        {
            emission.rateOverTime = idleEmissionRate;

            // Kuvveti sıfırla
            forceOverLifetime.x = new ParticleSystem.MinMaxCurve(0);
            forceOverLifetime.y = new ParticleSystem.MinMaxCurve(0);

            // Dumanı daha şeffaf hale getir
            main.startColor = idleColor; // Şeffaf bir renk (belli belirsiz duman)
        }

        // Mevcut pozisyonu güncelle
        previousPosition = playerTransform.position;
    }
}
