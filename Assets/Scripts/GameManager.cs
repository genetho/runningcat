using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton: make sure there is only 1 instance exist at any given time
    public static GameManager Instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrement = 0.1f;

    // UI
    public TextMeshProUGUI gameOverText;
    public Button retryButton;
    public Image summaryPanel;
    public Image scorePanel;
    public Button mainMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI highScoreLabel;

    private Spawner spawner;
    private Player player;

    private float score;
    public float gameSpeed { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        // Prevent null-reference
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        player = FindObjectOfType<Player>();

        NewGame();
    }

    public void NewGame()
    {
        // Remove every obstacle that was left on the screen
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (Obstacle obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        score = 0f;
        enabled = true;

        spawner.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        summaryPanel.gameObject.SetActive(false);
        scorePanel.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        highscoreText.gameObject.SetActive(false);
        highScoreLabel.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);

        UpdateHighScore();
    }

    public void GameOver()
    {
        // Disable everything
        gameSpeed = 0f;
        enabled = false;

        spawner.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        summaryPanel.gameObject.SetActive(true);
        scorePanel.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);
        highScoreLabel.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        UpdateHighScore();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrement * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("hiscore", 0f);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("hiscore", highScore);
        }

        highscoreText.text = Mathf.FloorToInt(highScore).ToString("D5");
    }
}
