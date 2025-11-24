using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// скрипт для рандомного расположения объектов на карте
/// </summary>
public class Swap : MonoBehaviour
{
    // Массив объектов, которые нужно перемешать на сцене
    public GameObject[] objectsToShuffle;

    void Start()
    {
        ShuffleObjects();
    }

    // Метод для перемешивания объектов
    void ShuffleObjects()
    {
        // Список для хранения позиций объектов
        List<Vector3> positions = new List<Vector3>();

        // Сохранение начальных позиции всех объектов
        foreach (GameObject obj in objectsToShuffle)
        {
            positions.Add(obj.transform.position);
        }

        // Перемешиваиние позиций
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 tempPosition = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = tempPosition;
        }

        // Присваиваивание новыъ позиций
        for (int i = 0; i < objectsToShuffle.Length; i++)
        {
            objectsToShuffle[i].transform.position = positions[i];
        }
    }
}
