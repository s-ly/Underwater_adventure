using UnityEngine;
using UnityEngine.EventSystems;

// Eправление кнопкой стрельбы (b3)
public class b3 : MonoBehaviour, IPointerDownHandler
{
    public GameObject myPlayer; // ячейка для Player

    // вызывается при нажатии
    public void OnPointerDown(PointerEventData eventData)
    {
        myPlayer.GetComponent<Player>().FireRoket(); // вызываем метод стрельбы у игрока
    }
}
