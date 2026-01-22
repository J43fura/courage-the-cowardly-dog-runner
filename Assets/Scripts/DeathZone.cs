using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;

    public Transform player;
    public float fixedYPosition = 0; // Set your desired fixed Y position
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            deathSound.Play();
            GameUIManager.instance.TriggerGameOver();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // keep fixed Y
            transform.position = new Vector3(
                player.position.x,
                fixedYPosition,
                player.position.z
            );
        }
    }
}
