using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// управление врагом типа "Барьер".
// Его нельзя убить.
public class BarrierEnemy : MonoBehaviour
{
    public float speedEnemy = -1f; // скорость врага
    public GameObject instExplosion; // ссылка на префаб взрыва

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
        
        // столкновение с ракетой
        if (other.gameObject.CompareTag("roket"))
        {
            Debug.Log("Бум!");
            EnemyCreate(); // БУМ!
            //Destroy(gameObject); //удалаем врага
        }

        // столкновение с игроком
        if (other.gameObject.CompareTag("player"))
        {
            Debug.Log("game ower my frend");
            EnemyCreate(); // БУМ!
            //Destroy(gameObject); //удалаем врагаs
        }
    }

    //создание взрыва
    void EnemyCreate()
    {
        Transform enemyTransform = this.GetComponent<Transform>(); // берём Трансформ app (себя)
        Instantiate(instExplosion, enemyTransform.position, enemyTransform.rotation); // создаём клон взрыва (он сам удаляется)
    }
    
}
