using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт для воспроизведения музыки 
/// </summary>
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Menu")
        {
            PlayMusic(menuMusic);
        }
        else if (currentScene == "SampleScene")
        {
            PlayMusic(gameMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

}
