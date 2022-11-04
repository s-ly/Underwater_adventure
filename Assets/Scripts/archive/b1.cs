/*
using UnityEngine;
using UnityEngine.EventSystems;

// управление кнопкой, которая двигает игрока влево.
// устаревшая версия.
// Обращаемся к полю, отвечающему за скорость игрока, в другом скрипте (игрока).
// поле "moveSpeed", скрипт "Player.cs".
public class b1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject myPlayer; // ячейка для Player
    public float moveSpeedDelda = -1f; // изменение скорости игрока
    
    // вызывается при нажатии
    public void OnPointerDown(PointerEventData eventData)
    {        
        myPlayer.GetComponent<Player>().moveSpeed = moveSpeedDelda; // меняем скорость игрока (движение влево)
    }

    // вызывается при отпускании (надеюсь)
    public void OnPointerUp(PointerEventData eventData)
    {        
        myPlayer.GetComponent<Player>().moveSpeed = 0f; // меняем скорость игрока (стоп)
    }
}
*/