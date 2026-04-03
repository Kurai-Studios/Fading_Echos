using UnityEngine;
using Unity.Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] float gameTimer = 120f;
    [SerializeField] TextMeshProUGUI timerText;
    float currentTime;

    THealthManager healthManager;
    bool gameEnded = false;

    private void Awake()
    {
        Camera.main.gameObject.AddComponent<CinemachineBrain>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentTime = gameTimer;
        healthManager = FindFirstObjectByType<THealthManager>();
    }

    private void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;

        UpdateTimer();

        if (currentTime <= 0)
            Win();

        if (healthManager != null)
            if (healthManager.health <= 0) Lose();
    }

    public void UpdateTimer()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void Win()
    {
        gameEnded = true;
        Debug.Log("YOU WIN!");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Lose()
    {
        gameEnded = true;
        Debug.Log("YOU LOST!");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
