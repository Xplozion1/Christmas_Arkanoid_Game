using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// скрипт движения шарика по игровому полю
/// </summary>
public class Candy : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Sprite DamageStar;

    [SerializeField]
    Sprite DamageTree1;

    [SerializeField]
    Sprite DamageTree2;
    [SerializeField] private float startSpeed = 7f;

    /// <summary>
    /// звуки
    /// </summary>
    [SerializeField] private AudioClip gift;
    [SerializeField] private AudioClip star;
    [SerializeField] private AudioClip tree;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        randomDirection.y = Mathf.Abs(randomDirection.y);
        rb.linearVelocity = randomDirection * startSpeed;
    }
    void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;

        // Проверка на вертикальное движение (x = 0)
        if (Mathf.Approximately(v.x, 0f))
        {
            // добавляем горизонтальное положение
            v.x = Random.Range(0.3f, 1f) * (Random.value > 0.5f ? 1f : -1f);
            v = v.normalized * startSpeed;
            rb.linearVelocity = v;
        }

        if (Mathf.Abs(v.y) < startSpeed)
        {
            v.y = Mathf.Sign(v.y) * startSpeed;
            rb.linearVelocity = v;
        }
    }

    /// <summary>
    /// проверка столкновений шарика с тегами и их уничтожение
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lose"))
        {
            Debug.Log("Вы проиграли!");
            GameManager.Instance.OnLose();
        }
        if (collision.gameObject.CompareTag("Block") ||
            collision.gameObject.CompareTag("Star1") ||
            collision.gameObject.CompareTag("Tree1"))
        {

            if (collision.gameObject.CompareTag("Block"))
                PlaySound(gift);
            else if (collision.gameObject.CompareTag("Star1"))
                PlaySound(star);
            else if (collision.gameObject.CompareTag("Tree1"))
                PlaySound(tree);

            Destroy(collision.gameObject);
            GameManager.Instance.OnObjectDestroyed();
        }

        if (collision.gameObject.CompareTag("Star2"))
        {
            collision.gameObject.tag = "Star1";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = DamageStar;
            rb.linearVelocity = new Vector2(Random.Range(6f, 7f), Random.Range(6f, 7f));
        }
        if (collision.gameObject.CompareTag("Tree2"))
        {
            collision.gameObject.tag = "Tree1";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = DamageTree1;
            rb.linearVelocity = new Vector2(Random.Range(6f, 7f), Random.Range(6f, 7f));
        }
        if (collision.gameObject.CompareTag("Tree3"))
        {
            collision.gameObject.tag = "Tree2";
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = DamageTree2;
            rb.linearVelocity = new Vector2(Random.Range(7f, 7f), Random.Range(7f, 7f));
        }
    }

    /// <summary>
    /// звуки
    /// </summary>
    /// <param name="clip"></param>
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
