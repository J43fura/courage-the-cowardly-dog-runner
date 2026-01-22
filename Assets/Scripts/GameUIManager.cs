using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    [Header("Player Stats")]
    public int heartCount = 3;
    public int slabCount = 0;
    public float score = 0f;
    public float bestScore = 0f; // Stores the highest score

    [Header("UI Elements")]
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI slabText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText; // Optional display

    public GameObject gameOverScreen; // Assign in Inspector

    [Header("Fog Settings")]
    public bool fogEnabled = false; // Toggle in Inspector or via code

    [Header("Bomb Settings")]
    public bool bombsEnabled = true; // Toggle bombs independently

    [ContextMenu("Reset Best Score")]
    public void ResetBestScore()
    {
        PlayerPrefs.DeleteKey("BestScore");
        PlayerPrefs.Save();

        bestScore = 0;
        if (bestScoreText != null)
        {
            bestScoreText.gameObject.SetActive(true);
            bestScoreText.text = "0";
        }
    }

    private bool isGameOver = false;

    private Transform player;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Load saved best score
        bestScore = PlayerPrefs.GetFloat("BestScore", 0f);
    }

    void Start()
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false); // Ensure it is hidden at start

        UpdateUI();
        ApplyFog(); // Set fog at start based on fogEnabled

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (!isGameOver && player != null)
        {
            // Score based on forward distance (z-axis)
            score = player.position.z + slabCount * 50; // Each slab adds 50 points
            scoreText.text = Mathf.FloorToInt(score).ToString();

            // Update best score dynamically
            if (score > bestScore)
            {
                bestScore = score;
                PlayerPrefs.SetFloat("BestScore", bestScore);
                PlayerPrefs.Save();

                bestScoreText.text = "";
            }
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
        if (this.AreBombsEnabled()) heartText.text = heartCount.ToString();
        slabText.text = slabCount.ToString();

        if (bestScoreText != null)
            bestScoreText.text = Mathf.FloorToInt(bestScore).ToString();
    }

    public void SetFog(bool enabled)
    {
        fogEnabled = enabled;
        ApplyFog();
    }

    private void ApplyFog()
    {
        RenderSettings.fog = fogEnabled;
    }

    public void SetBombs(bool enabled)
    {
        bombsEnabled = enabled;
    }

    public bool AreBombsEnabled()
    {
        return bombsEnabled;
    }

}
