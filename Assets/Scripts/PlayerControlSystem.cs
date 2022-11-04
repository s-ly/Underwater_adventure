using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // для работы с событиями

/* Система управления перемещением игрока по экрану с помощью касания.
Скрипт вешается на пустышку-группу, в которую входят плоскость и временная цель. Так-как управление происходит
через 3д-объекты, на камере должнен быть компонент Physics Raycaster, а в сцене EventSystem. Для работы 
требуются подключить итерфейсы. Ещё скрипт вычисляег границы экрана, что бы игок не мог зайти за них.*/
public class PlayerControlSystem : MonoBehaviour,
    IPointerDownHandler, 
    IDragHandler
{
    /* Границы экрана в 3д пространстве, относительно 2д экрана,
     а глубина будет лежать в 0 по оси z.*/
    float screenBoundaryLeft;
    float screenBoundaryRight;
    float screenBoundaryTop;
    float screenBoundaryBottom;    

    // ссылка на игрока, которого двигаем и временную мишень.
    public GameObject Player;
    public GameObject TempTarget;

    public Camera Cam; // камера уровня
    private Vector3 offset; // для хранения смещения (между игроком и временной целью)

    private void Start()
    {
        CalculatingScreenBoundaries();
    }

    // ИНТЕРФЕЙС - событие при нажатии.
    // При нажатии на Плоскость получаем координаты нажатия в мировом пространстве.
    // Туда ставим временную цель, только по оси x и y.
    // Это происходит в самом начале, до непосредственного перетаскивания,
    // как бы подготовка к перетаскиванию. Вообще есть специальный интерфейс,
    // который как бы для этого, но он даёт рывки.
    // Затем вычисляем разницу между Игроком и временной целью.
    public void OnPointerDown(PointerEventData eventData)
    {
        
        // вычисляем место нажатия
        float cursorPositionX = eventData.pointerCurrentRaycast.worldPosition.x;
        float cursorPositionY = eventData.pointerCurrentRaycast.worldPosition.y;
        
        // проверка не равен ли курсор нулевым координатам нужня, так-как
        // когда курсор касается края экрана или какой нибудь кнопки, он становится равен нулю,
        // и курсор с Игроком прыгуют в ноль, а так просто стоят на месте.
        if(cursorPositionX != 0 && cursorPositionY != 0)
        {
            // ставим временную цель
            TempTarget.transform.position = new Vector3(cursorPositionX, cursorPositionY, 0);

            // вычисляем смещение временной цели от игрока, с проверкой.
            // Если игрок умер Destroy(gameObject); то работатьс ним нельзя
            if (Player != null) offset = Player.transform.position - TempTarget.transform.position;
        }
    }

    // ИНТЕРФЕЙС - событие перетаскивание.
    // Теперь можно тащить, так же ставим временную цель на место события,
    // но теперь событие происходит постоянно и наша временная цель переставляется.
    // Следующим шагом пододвигаем Игрока в временной цели, но с учётом смещения,
    // которое вычислили заранее. Всё это нужно для того, что бы Игрок плавно двигался,
    // а не возникал там, куда мы нажали на экране.
    public void OnDrag(PointerEventData eventData)
    {
        // вычисляем место нажатия
        float cursorPositionX = eventData.pointerCurrentRaycast.worldPosition.x;
        float cursorPositionY = eventData.pointerCurrentRaycast.worldPosition.y;

        // проверка не равен ли курсор нулевым координатам нужня, так-как
        // когда курсор касается края экрана или какой нибудь кнопки, он становится равен нулю,
        // и курсор с Игроком прыгуют в ноль, а так просто стоят на месте.
        if (cursorPositionX != 0 && cursorPositionY != 0)
        {
            // ставим временную цель
            TempTarget.transform.position = new Vector3(cursorPositionX, cursorPositionY, 0);

            // ставим Игрока с учётом смещения, с проверкой.
            // Если игрок умер Destroy(gameObject); то гвигать его нельзя
            if (Player != null)
            {
                Player.transform.position = TempTarget.transform.position + offset;
                AdjustingPositionAtBoundaries();
            }
        }
    }

    /* Вычисление краёв экрана.
    В основе метод ScreenToWorldPoint(). Он принимаем 2д-координаты экрана, а возвращает 3д-координаты мира.
    Также ему надо передать 3й аргумент, рассотяние от камеры до плостости в 3д-пространстве, в которой лежат
    найденные 3д-координаты. 2д-координаты лежат от 0, 0 - нижний леый угол и Cam.pixelWidth и Cam.pixelHeight,
    это ширина экрана и высота (или глубина, надо уточнить) в пикелях вроде. В случае Left мы переводим в минус.
    Так-как нужная нам плоскость лежит на нуле, мы просто передаём третьим параметром удаление камеры по z от 0,
    или просто z камеры. Что бы найти левый край, 3д-точка лежит по середине экрана по высоте, а для поиска
    верхних и нижнихграниц, ставим по середине горизонтальную точку.*/
    void CalculatingScreenBoundaries()
    {
        screenBoundaryLeft = -((Cam.ScreenToWorldPoint(new Vector3(0, Cam.pixelHeight / 2, Cam.transform.position.z))).x);
        screenBoundaryRight = (Cam.ScreenToWorldPoint(new Vector3(1, Cam.pixelHeight / 2, Cam.transform.position.z))).x;
        screenBoundaryTop = (Cam.ScreenToWorldPoint(new Vector3(Cam.pixelWidth / 2, 1, Cam.transform.position.z))).y;
        screenBoundaryBottom = (Cam.ScreenToWorldPoint(new Vector3(Cam.pixelWidth / 2, Cam.pixelHeight, Cam.transform.position.z))).y;
    }
    
    /* Корректировка положения игрока около края экрана. 
     Теперь мы знаем координаты краёв экрана. Если игрок залезает за них,
    мы просто возвражаем его на край, для каждого края независимо.*/
    void AdjustingPositionAtBoundaries()
    {
        if (Player.transform.position.x <= screenBoundaryLeft)
            Player.transform.position = new Vector3(screenBoundaryLeft, Player.transform.position.y, Player.transform.position.z);
        if (Player.transform.position.x >= screenBoundaryRight)
            Player.transform.position = new Vector3(screenBoundaryRight, Player.transform.position.y, Player.transform.position.z);
        if (Player.transform.position.y >= screenBoundaryTop)
            Player.transform.position = new Vector3(Player.transform.position.x, screenBoundaryTop, Player.transform.position.z);
        if (Player.transform.position.y <= screenBoundaryBottom)
            Player.transform.position = new Vector3(Player.transform.position.x, screenBoundaryBottom, Player.transform.position.z);
    }
}
