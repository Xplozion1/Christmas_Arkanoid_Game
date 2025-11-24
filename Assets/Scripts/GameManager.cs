using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Скрипт определения победы/поражения игрока, воспроизведение звуков
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Панели
    [SerializeField] private GameObject gameOverPanel; // Панель проигрыша
    [SerializeField] private GameObject gameWinPanel;  // Панель победы

    // Кнопки для панели проигрыша (Lose)
    [SerializeField] private Button loseRestartButton;
    [SerializeField] private Button loseExitButton;

    // Кнопки для панели победы (Win)
    [SerializeField] private Button winRestartButton;
    [SerializeField] private Button winExitButton;
    //звуки победы и поражения
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    private int totalBlocks;
    private int destroyedBlocks;
    private AudioSource audioSource;
    private void Awake()
    {
        //удаляем дубликаты GameManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        destroyedBlocks = 0;
        audioSource = GetComponent<AudioSource>();
        // Считаем количество объектов с тегами
        totalBlocks = GameObject.FindGameObjectsWithTag("Block").Length +
                      GameObject.FindGameObjectsWithTag("Tree1").Length +
                      GameObject.FindGameObjectsWithTag("Tree2").Length +
                      GameObject.FindGameObjectsWithTag("Tree3").Length +
                      GameObject.FindGameObjectsWithTag("Star1").Length +
                      GameObject.FindGameObjectsWithTag("Star2").Length;

        loseRestartButton.onClick.AddListener(RestartGame);
        loseExitButton.onClick.AddListener(ExitGame);

        winRestartButton.onClick.AddListener(RestartGame);
        winExitButton.onClick.AddListener(ExitGame);

        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void OnObjectDestroyed()
    {
        destroyedBlocks++;
        Debug.Log($"Destroyed: {destroyedBlocks}/{totalBlocks}");

        if (destroyedBlocks >= totalBlocks)
        {
            GameWin(); // Победа
        }
    }

    public void OnLose()
    {
        GameOver(); // Проигрыш
    }

    private void GameOver()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }
        PlaySound(lose);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Останавливаем игру
    }

    private void GameWin()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.StopMusic();
        }
        PlaySound(win);
        gameWinPanel.SetActive(true);
        Time.timeScale = 0f; // Останавливаем игру
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Переход в главное меню
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
