using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private AudioSource tileSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tile touched");
            tileSound.pitch = Random.Range(0.1f, 1.9f);
            tileSound.Play();
        }
    }
}
