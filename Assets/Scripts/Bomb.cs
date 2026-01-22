using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    [SerializeField] private AudioSource explosionSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(explosionSound.clip, transform.position);

            GameObject explosion = Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
            Destroy(explosion, 3f);
        }
    }
}
