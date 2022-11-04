using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// управление ракетой.
// Ракета сама проверяет, с чем она столкнулась.
public class roket : MonoBehaviour
{
    public float speedRoket = 1f; // скорость ракеты
    GameObject app; // ссылка на главный код (нельзя прикрепить в ручную, надо кодом)
    
    // при старте
    private void Start()
    {
        app = GameObject.FindWithTag("app"); // ссылка на главный код app (так как нельзя руками)        
    }

    // UNITY Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, speedRoket, 0) * Time.deltaTime); // перемещаем ракету
    }

    /* Обнаружение столкновения ракеты, с чем она столкнулась. У ракеты есть компонент Capsule Collider.
    У стенок и врагов есть Box Collider (is Trigger - on) и RigidBody (is Kinematic - on).
    Вызываетя при столкновении с дргим колаэдром, где Collider other - ссылка на другой объект с колаедром.
    По ссылкена на полученный объект проверяем у него Тег, дальше по обстаятельствам.*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gameEnd")) Destroy(gameObject); // удалаем ракету
        if (other.gameObject.CompareTag("enemy")) Destroy(gameObject); // удалаем ракету
    }
}
