using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTouch : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Получаем информацию о первом касании
            if (touch.phase == TouchPhase.Ended) // Проверяем, завершилось ли касание
            {
                Debug.Log("Произошло нажатие");
            }
        }
    }
}
