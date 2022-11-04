using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// управление врагом
public class enemy_OLD : MonoBehaviour
{
    public float speedEnemy = -1f; // скорость врага
    GameObject app; // ссылка на главный код (нельзя прикрепить в ручную, надо кодом)
    public GameObject instExplosion; // ссылка на префаб взрыва
    public GameObject instStar; // ссылка на префаб звезды
    //float gameSpeedApp; // глобальная скорость игры (берём у app)

    // при старте
    private void Start()
    {
        app = GameObject.FindWithTag("app"); // ссылка на главный код app (так как нельзя руками)        
        //gameSpeedApp = app.GetComponent<app>().gameSpeed; // берём глобальную скорость игры
    }

    // UNITY Update is called once per frame
    void Update()
    {
        // метод перемещения врага (с учётом gameSpeedApp)
        this.transform.Translate(new Vector3(0, speedEnemy * Data.staticDataGameSpeed, 0) * Time.deltaTime); 
    }

    // Обнаружение столкновения врага и со стенкой и с торпедой.
    // У стенки и у врага есть Box Collider (is Trigger - on) и RigidBody (is Kinematic - on).
    // Ещё проверяем тригеры.
    private void OnTriggerEnter(Collider other)
    {
        // проверяем, с чем столкнулись, и стоит ли из-за этого погибать.
        // Проверка нужна, так-как можем столкнуться с плоскостью контроллера управления игроком,
        // а её нужно игнорировать.
        // Ещё, если столкнулись с границей игры, то генерироать взрыв не надо.
        if (other.gameObject.CompareTag("gameEnd"))
        {
            Debug.Log("границы игры");
            Destroy(gameObject); //удалаем врага
        }        
        
        // столкновение с ракетой или игроком
        if (other.gameObject.CompareTag("roket") || other.gameObject.CompareTag("player"))
        {
            Debug.Log("Бум!");
            EnemyCreate(); // БУМ!   
            StarCreate(); // Звёзды посыпали
            app.GetComponent<app>().workDestroyEnemy(); //вызываем метод работы со счётчиком уничтоженных врагов главного кода
            Destroy(gameObject); //удалаем врага
        }
    }

    //создание взрыва
    void EnemyCreate()
    {
        Transform enemyTransform = this.GetComponent<Transform>(); // берём Трансформ app (себя)
        // Корректируем положение появления нового врага, поднимаем врага наверх и случайно сдвигаем по x.
        Instantiate(instExplosion, enemyTransform.position, enemyTransform.rotation); // создаём клон взрыва (он сам удаляется)
    }

    // создание звёзд
    void StarCreate()
    {
        Transform starTransform = this.GetComponent<Transform>(); // берём Трансформ app (себя)
        Instantiate(instStar, starTransform.position, starTransform.rotation); // создаём клон звезды
    }
}
