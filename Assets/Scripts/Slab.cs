using UnityEngine;

public class Slab : MonoBehaviour
{
    [SerializeField] private AudioSource slabSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(slabSound.clip, transform.position);
        }
    }
}

