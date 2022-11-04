using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Управляет взрывом.
public class explosion : MonoBehaviour
{

    public float speedEnemy = -1f; // скорость игры (для редактора)

    // UNITY Update is called once per frame
    void Update()
    {
        // метод перемещения с учётом staticDataGameSpeed
        this.transform.Translate(new Vector3(0, speedEnemy * Data.staticDataGameSpeed, 0) * Time.deltaTime);
    }

    // В конце анимации спрайта стоит event, он вызывает этот метод.
    public void aniEvent(string message)
    {        
        Destroy(gameObject); // убиваем взрыв.
    }
}
