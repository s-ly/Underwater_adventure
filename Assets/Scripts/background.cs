using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // для интерфейса 

// Анимация UV фона.
public class background : MonoBehaviour
{
    public Text DL2;

    public float speedScrollUv = 0.5f; // скорость анимации UV фона
    //public float speedScrollUv; // скорость анимации UV фона
    //GameObject app; // на главный код
    //float gameSpeedApp; // глобальная скорость игры (берём у app)

    private void Awake()
    {
        //gameSpeedApp = app.GetComponent<app>().gameSpeed; // берём глобальную скорость игры

    }


    // Start is called before the first frame update
    void Start()
    {
        //app = GameObject.FindWithTag("app");
        //gameSpeedApp = app.GetComponent<app>().gameSpeed; // берём глобальную скорость игры
    }

    // Update is called once per frame
    void Update()
    {
        // Вычисляем новое смещение координат в каждом кадре ( с учётом gameSpeedApp).
        float scrollUv = Time.time * speedScrollUv * Data.staticDataGameSpeed;
        //Debug.Log("DB!!!!!!" + speedScrollUv + " " + gameSpeedApp);
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, scrollUv); // смещаем текстуру

        DL2.text = Data.staticDataGameSpeed.ToString();
    }
}
