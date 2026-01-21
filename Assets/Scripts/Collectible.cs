using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.name.Contains("Bomb"))
            {
                GameUIManager.instance.LoseHeart();
            }
            else if (gameObject.name.Contains("Slab"))
            {
                GameUIManager.instance.AddSlab();
            }
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
