using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Хранение данных, способных передаваться и сохраняться между сценами.
При выключении обнуляются. Особый скрипт, его не нужно надевать на GameObject.
В нём используются статические поля, они (вроде) общие для всех экземпляров.
В скриптак нужно непосредственно обращаться к полям.
Так же есть статические методы для работы с загрузкой данных из реестра ОС.*/
public class Data : MonoBehaviour
{
    public static int staticDataGameLevel; // уровень игры
    public static int staticDataGameResult; // выиграл игрок или проиграл
    public static int staticDataSumDestroyEnemy; // кол-во уничтоженных врагов
    public static int staticDataSumCollectedStars; // кол-во собранных звёзд

    /* Скорость игры, специальный коефициент, меняет скорость перемещения 
    врагов, скорость перемещения фона и скорость генерации врагов.
    Мы её меняем в зависимости от выбора игроком уровня.
    В новой верисии хранив в staticData.*/
    public static float staticDataGameSpeed = 1f; // скорость игры

    /*Рекордное количество собранных звёзд за раунд.
     Это поле хранится в реестре ОС и сохраняется при перезапуске игры.*/
    public static int staticDataPlayerPrefs_StarRecord;    

    /*Загрузка данных из реестра ОС. Вызываю в app при старте игры.
     Если записи ещё нет, инициируем нулём.*/
    public static void LoadPlayerPrefsData()
    {
        // если запись есть, то загрузим её
        if (PlayerPrefs.HasKey("starRecord")) 
        {
            // если запись есть, то загрузим её
            Debug.Log("запись в реестре есть, загружаем её");
            staticDataPlayerPrefs_StarRecord = PlayerPrefs.GetInt("starRecord");
        }
        // иначе создадим новую с нулевым значением
        else
        {
            Debug.Log("нет записи в реестре, тогда присвоим 0 и запишим в реестр ОС");
            staticDataPlayerPrefs_StarRecord = 0;
            PlayerPrefs.SetInt("starRecord", staticDataPlayerPrefs_StarRecord);
        }
    }
}
