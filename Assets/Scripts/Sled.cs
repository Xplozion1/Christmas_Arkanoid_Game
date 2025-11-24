using UnityEngine;
/// <summary>
/// скрипт управления платформой (санками)
/// </summary>
public class Sci : MonoBehaviour
{
    [SerializeField]
    [Range(10f, 100f)]
    float speed = 20f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*Vector3.right*Input.GetAxis("Horizontal")*Time.deltaTime);
        transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -6.7f, 6.7f), transform.position.y, transform.position.z);
    }
}
