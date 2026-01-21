using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public int heartCount = 3;
    public int slabCount = 0;

    public TextMeshProUGUI heartText;
    public TextMeshProUGUI slabText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddSlab()
    {
        slabCount++;
        UpdateUI();
    }

    public void LoseHeart()
    {
        heartCount--;
        UpdateUI();
    }

    void UpdateUI()
    {
        heartText.text = heartCount.ToString();
        slabText.text = slabCount.ToString();
    }
}
