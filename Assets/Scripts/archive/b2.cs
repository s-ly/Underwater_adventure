/*
using UnityEngine;
using UnityEngine.EventSystems;

public class b2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject myPlayer; // ячейка для Player
    public float moveSpeedDelda = 1f; // изменение скорости игрока

    // вызывается при нажатии
    public void OnPointerDown(PointerEventData eventData)
    {        
        myPlayer.GetComponent<Player>().moveSpeed = moveSpeedDelda; // меняем скорость игрока (движение вправо)
    }

    // вызывается при отпускании (надеюсь)
    public void OnPointerUp(PointerEventData eventData)
    {        
        myPlayer.GetComponent<Player>().moveSpeed = 0f; // меняем скорость игрока (стоп)
    }
}
*/