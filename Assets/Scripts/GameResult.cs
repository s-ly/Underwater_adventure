using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // для интерфейса 

// Управляет сценой "GameResult".
public class GameResult : MonoBehaviour
{
    public Text textGameResult; // текст результата игры
    public Text textSumDestroyEnemy; // текст кол-во уничтоженных врагов
    public Text textSumCollectedStars; // текст кол-ва собранных звёзд
    public Text textStarRecord; // текст рекорда собранных звёзд
    private int gameResult; // сюда загрузим результат игры из data    
    private int sumDestroyEnemy; // сюда загрузим кол-во уничтоженных врагов после игры из data
    private int sumCollectedStars; // сюда загрузим кол-во собранных звёзд после игры из data
    private int starRecord; // сюда загрузим рекорд собранных звёзд после игры из data
    private string gameResultText; // текст сообщения об результате игры
    private string starRecordText; // сообщение о рекорде по звёздам

    // Start is called before the first frame update
    void Start()
    {
        StaticDataLoad();
        CheckGameResult();
        PrintGameResult();
    }

    /*Загрузка данных из staticData*/
    void StaticDataLoad()
    {
        gameResult = Data.staticDataGameResult; // результат игры
        sumDestroyEnemy = Data.staticDataSumDestroyEnemy; // кол-во уничтоженных врагов после игры
        sumCollectedStars = Data.staticDataSumCollectedStars; // кол-во собранных звёзд после игры
        starRecord = Data.staticDataPlayerPrefs_StarRecord; // рекорд собранных звёзд после игры
    }
    
    /* Вывод результатов на экран.
    Кол-во уничтоженных врагов, собранных звёзд после игры и рекорд звёзд. */
    void PrintGameResult()
    {
        textGameResult.text = gameResultText;
        textStarRecord.text = starRecordText;
        textSumDestroyEnemy.text = "Destroy enemy: " + sumDestroyEnemy.ToString();
        textSumCollectedStars.text = "Stars: " + sumCollectedStars.ToString();
    }

    // Проверка результатов игры и формированиее сообщений
    void CheckGameResult()
    {
        // проверка, нет ли рекорда по звёздам
        if(sumCollectedStars > starRecord)
        {
            Debug.Log("Новый рекорд!");
            starRecord = sumCollectedStars; // обновляем локальное значение
            Data.staticDataPlayerPrefs_StarRecord = starRecord; // обновляем staticData значение
            PlayerPrefs.SetInt("starRecord", Data.staticDataPlayerPrefs_StarRecord); // обновляем значение в реестре ОС
            starRecordText = "Новый рекорд! " + starRecord.ToString();
        }
        else
        {
            Debug.Log("Старый рекорд");
            starRecordText = "Старый рекорд: " + starRecord.ToString();
        }

        // проверка результата игры на выигрыш
        if (gameResult == 0)
        {
            gameResultText = "You lost";
        }
        else
        {
            gameResultText = "You win";
        }
    }
}
