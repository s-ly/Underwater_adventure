using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // для интерфейса 
using UnityEngine.SceneManagement; //загрузка сцены

// Главный игровой скрипт 
public class app : MonoBehaviour
{
    public Text DL1;

    private int gameLevel = 1; // Какой уровень игры включить (возьмём из staticData)
    public GameObject instEnemy; // ссылка на префаб врага
    public GameObject instBarrierEnemy; // ссылка на префаб врага типа "Барьер"
    public Text textCollectedStars; // ссылка на текст счётчика собранных звёзд
    public Text restPlayTime; // ссылка на текст: остаток времени игры
    private int sumDestroyEnemy = 0; // кол-во уничтоженных врагов
    private int sumCollectedStars = 0; // кол-во собранных звёзд
    private float enemyCreateTimerPeriod = 1.5f; // для таймера генерации врагов
    private float enemyCreateTimerPeriodTemp; // для таймера генерации врагов (временная,так-как основная изменяется в коде)
    
    public float gamePlayTime = 20f; // время игры (продолжительность) задаю в редакторе    
    public float gamePlayTimer = 0f; // таймер игры (наращивается со временем )     
    public float gamePlayTimePercent; // время игры в процентах
    
    public bool flagGameOwer = false; // флаг, если игрок погибает то true
    public bool flagGameWin = false; // флаг, если игрок выиграл то true
    public GameObject TextGameOwer; // ссылка на надпись Game Over
    public GameObject TextGameWin; // ссылка на надпись Game Win
    public GameObject instStar; // ссылка на префаб звезды

    // обратный отсчёт после гибели игрока, до вывода главного меню
    private float gameOwerPeriodTime = 2.5f;

    private void Start()
    {
        Data.LoadPlayerPrefsData(); // загружаем данные из реестра ОС
        
        //были в Awake()
        gameLevel = Data.staticDataGameLevel; // забираем из staticData какой уровень выбрал пользователь
        LevelGenerate(gameLevel); // генерируем соотвецтвующий выбору уровень

        // коректируем период появления врагов в зависимости от глобальной скорости игры
        enemyCreateTimerPeriod = enemyCreateTimerPeriod / Data.staticDataGameSpeed;
        // копируем значение во временную переменную
        enemyCreateTimerPeriodTemp = enemyCreateTimerPeriod;
    }

    void Update()
    {
        timerEnemyCreate(); // не пришло ли время создать врага, если пришло, то создаём.
        GameWinPerioTime(); // обновляем счётчик времени игры и проверяем его. (он включит флаг победы)

        // включаем обратный отсчёт после гибели игрока (контролирует флаг)
        if (flagGameOwer) 
        {
            // пока таймер отсчитывается, вывидем надпись Game Over (если уже не выведен)
            if (TextGameOwer.GetComponent<Text>().enabled != true)
                TextGameOwer.GetComponent<Text>().enabled = true;

            GameOwerPeriodTime();
        }

        // включаем обратный отсчёт после выигрыша игрока (контролирует флаг)
        if (flagGameWin)
        {
            // пока таймер отсчитывается, вывидем надпись Game Win (если уже не выведен)
            if (TextGameWin.GetComponent<Text>().enabled != true)
                TextGameWin.GetComponent<Text>().enabled = true;

            GameOwerPeriodTime();
        }

        DL1.text = Data.staticDataGameSpeed.ToString();
    }

    /*Генерируем уровень на основе выбора игрока.*/
    private void LevelGenerate(int selectGameLevel)
    {
        // На онове выбора игрока меняем скорость игры
        if (selectGameLevel == 1) Data.staticDataGameSpeed = 2f;
        if (selectGameLevel == 2) Data.staticDataGameSpeed = 4f;
        if (selectGameLevel == 3) Data.staticDataGameSpeed = 6f;
        if (selectGameLevel == 4) Data.staticDataGameSpeed = 8f;

        Debug.Log("select lavel: " + selectGameLevel + ", game speed: " + Data.staticDataGameSpeed);
    }

    // после гибели игрока нужно немного подождать, прежде чем
    // закончить игру и загрузить другую сцену с меню.
    // Когда таймер кончится, вызовем специальный метод для Game Over.
    // Используем и когда игрок победил (вышло время), как бы продержался.
    public void GameOwerPeriodTime()
    {
        gameOwerPeriodTime -= Time.deltaTime;
        if (gameOwerPeriodTime <= 0 && flagGameOwer) GameOver(0); //GAME OVER (LOST)
        if (gameOwerPeriodTime <= 0 && flagGameWin) GameOver(1); //GAME OVER (WIN)
    }

    // Таймер игры до победы (влючает флаг победы)
    public void GameWinPerioTime()
    {
        gamePlayTimer += Time.deltaTime; // наращиваем счётчик времени
        
        gamePlayTimePercent = (gamePlayTimer * 100) / gamePlayTime; // Считаем процент времени игры
        gamePlayTimePercent = Mathf.Round(gamePlayTimePercent); // округление до целого

        // выводим на экран остаток времени игры в процентах
        restPlayTime.text = "Level complete: " + gamePlayTimePercent.ToString() + "%";

        if (gamePlayTimePercent == 100) flagGameWin = true; // если прошло 100% игрового времени то "GAME OVER"
    }

    // Каждый заданный период времени запускаем создание врага.
    void timerEnemyCreate()
    {
        enemyCreateTimerPeriod -= Time.deltaTime;
        if (enemyCreateTimerPeriod <= 0)
        {
            Debug.Log("новый враг");
            EnemyCreate();
            // восстанавливаем значение прерывания из временной переменной
            enemyCreateTimerPeriod = enemyCreateTimerPeriodTemp; 
        }
    }

    //создание врага
    void EnemyCreate()
    {
        Transform enemyTransform = this.GetComponent<Transform>(); // берём Трансформ app (себя)
        // Корректируем положение появления нового врага, поднимаем врага наверх и случайно сдвигаем по x.
        enemyTransform.position = new Vector3(Random.Range(-3.0f, 3.0f), 14f, 0);

        // Теперь нужно выбрать тип врага (барьер или бомба)
        int typeEnemy = Random.Range(1, 5); // случайно тип врага (последнее не входит)
        switch (typeEnemy)
        {
            case 1:
                // создаём врага от instEnemy
                Instantiate(instEnemy, enemyTransform.position, enemyTransform.rotation); 
                break;
            case 2:
                // создаём врага от instEnemy
                Instantiate(instEnemy, enemyTransform.position, enemyTransform.rotation); 
                break;
            case 3:
                // создаём врага от instEnemy
                Instantiate(instEnemy, enemyTransform.position, enemyTransform.rotation); 
                break;
            case 4:
                // создаём врага "Барьер" от instEnemy
                Instantiate(instBarrierEnemy, enemyTransform.position, enemyTransform.rotation); 
                break;
        }
    }

    // работа со счётчиком уничтоженных врагов
    public void workDestroyEnemy()
    {
        Debug.Log("работа со счётчиком уничтоженных врагов");
        sumDestroyEnemy += 1; // увеличиваем счётчик
        //textCollectedStars.text = "Stars: " + sumDestroyEnemy.ToString(); // выводим на экран новый счёт
    }
    
    // работа со счётчиком собранных звёзд
    public void workCollectedStars()
    {
        sumCollectedStars += 1; // увеличиваем счётчик звёзд
        textCollectedStars.text = "Stars: " + sumCollectedStars.ToString(); // выводим на экран новый счёт
    }

    //Игра окончена, мой друг
    public void GameOver(int gameResult)
    {
        Data.staticDataGameResult = gameResult; // заносим в staticData результат игры (0 или 1)
        Data.staticDataSumDestroyEnemy = sumDestroyEnemy; // заносим в staticData кол-во уничтоженных врагов
        Data.staticDataSumCollectedStars = sumCollectedStars; //заносим в staticData кол-во собранных звёзд
        SceneManager.LoadScene("GameResult");
    }

    /* Cоздание звёзд. (вызываем у врагов).
    Метод принимает положение, где нужно создать звёзды. Случайно выбираем, сколько звсёзд выпадет.*/
    public void StarCreate(Transform starTransform)
    {
        Debug.Log("создание звезды из app");
        float starDeflect = 0.3f; // локальное отклонение новой звезды относительно убитого врага (отправим в рандом)
        int countStarsGenerate = Random.Range(1, 4); // скотлько звёзд выпадет (от 1 до 3)

        for (int i = 1; i <= countStarsGenerate; i++)
        {
            Instantiate(instStar,
                starTransform.position + new Vector3(Random.Range(-starDeflect, starDeflect), Random.Range(-starDeflect, starDeflect), 0),
                starTransform.rotation);
        }         
    }
}
