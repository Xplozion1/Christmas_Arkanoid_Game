using UnityEngine;
/// <summary>
/// скрипт анимации снежинок
/// </summary>
public class snow : MonoBehaviour
{
    public GameObject snowflakePrefab; 
    public float spawnRate = 0.1f;
    public float spawnAreaWidth = 10f; 
    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnSnowflake();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnSnowflake()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaWidth, spawnAreaWidth),
            transform.position.y
        );
        Instantiate(snowflakePrefab, spawnPosition, Quaternion.identity);
    }
}
