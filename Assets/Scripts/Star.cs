using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Управление звездой
public class Star : MonoBehaviour
{
    public float speedStar = -1f; // скорость звезды
    GameObject app; // ссылка на главный код (нельзя прикрепить в ручную, надо кодом)
    //float gameSpeedApp; // глобальная скорость игры (берём у app)

    // Start is called before the first frame update
    private void Start()
    {
        app = GameObject.FindWithTag("app"); // ссылка на главный код app (так как нельзя руками)        
        //gameSpeedApp = app.GetComponent<app>().gameSpeed; // берём глобальную скорость игры
    }

    // Update is called once per frame
    void Update()
    {
        // метод перемещения звезды (с учётом gameSpeedApp)
        this.transform.Translate(new Vector3(0, speedStar * Data.staticDataGameSpeed, 0) * Time.deltaTime); 
    }

    // Обнаружение столкновения звезды и со стенкой и с игроком.
    // У стенки есть Box Collider (is Trigger - on) и RigidBody (is Kinematic - on).
    // У игрока только Box Collider (is Trigger - off) и тэг player
    // Ещё проверяем тригеры.
    private void OnTriggerEnter(Collider other)
    {
        // столконовение с границей игры
        if (other.gameObject.CompareTag("gameEnd"))
        {
            Destroy(gameObject); //удалаем звезду
        }

        // столкновение с игроком
        if (other.gameObject.CompareTag("player"))
        {
            Debug.Log("Игрок поймал звезду");
            app.GetComponent<app>().workCollectedStars(); //вызываем метод работы со счётчиком собранных звёзд главного кода
            Destroy(gameObject); //удалаем звезду
        }
    }
}
