using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private AudioSource tileSound;
    private bool hasBeenTouched = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Tile touched");
            tileSound.pitch = Random.Range(0.8f, 1.2f);
            tileSound.Play();
            hasBeenTouched = true;
        }
    }
}
