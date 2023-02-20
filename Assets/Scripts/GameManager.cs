using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton: make sure there is only 1 instance exist at any given time
    public static GameManager Instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrement = 0.1f;
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
        NewGame();
    }

    private void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrement * Time.deltaTime;
    }
}
