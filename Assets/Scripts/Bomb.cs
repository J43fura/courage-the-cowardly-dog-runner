using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    [SerializeField] private AudioSource explosionSound;

    private bool exploded = false;

    void OnTriggerEnter(Collider other)
    {
        if (exploded) return;

        if (other.CompareTag("Player"))
        {
            exploded = true;

            // Play sound safely
            if (explosionSound != null)
            {
                explosionSound.enabled = true;   // make sure it's enabled
                explosionSound.Play();
            }

            // Spawn explosion
            if (explosionEffect != null)
            {
                GameObject explosion = Instantiate(
                    explosionEffect,
                    transform.position,
                    Quaternion.identity
                );
                Destroy(explosion, 3f);
            }
        }
    }
}
