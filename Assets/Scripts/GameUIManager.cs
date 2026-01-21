using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public int heartCount = 3;
    public int slabCount = 0;
    public float score = 0f;

    public TextMeshProUGUI heartText;
    public TextMeshProUGUI slabText;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverScreen; // Assign in Inspector

    private bool isGameOver = false;
    private Transform player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false); // Ensure it is hidden at start

        UpdateUI();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (!isGameOver && player != null)
        {
            // Score based on forward distance (z-axis)
            score = player.position.z;
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }

        // Restart game if GameOver and any key pressed (Input System)
        if (isGameOver && Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddSlab()
    {
        slabCount++;
        UpdateUI();
    }

    public void LoseHeart()
    {
        if (isGameOver) return; // Prevent calling multiple times

        heartCount--;
        UpdateUI();

        if (heartCount <= 0)
        {
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Prevent multiple calls

        isGameOver = true;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        // Optional: stop player movement
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
                controller.enabled = false;
        }
    }

    void UpdateUI()
    {
        heartText.text = heartCount.ToString();
        slabText.text = slabCount.ToString();
    }
}
