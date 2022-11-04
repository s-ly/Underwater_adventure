using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject app; // на главный код
    public GameObject instRoket; // на префаб ракеты
    public float periodFire = 0.5f; // время между выстрелами (скорость стрельбы)
    public float periodFireTemp; // временная переменная для таймера (скорость стрельбы)

    //public float moveSpeed = 0f; //корость перемещения игрока (меняется кнопкой)   

    /*
    // для таймера выстрелов
    private float nextActionTime = 0.0f;
    public float periodFire = 0.5f;
    */

    // обратный отсчёт после гибели игрока, до вывода главного меню
    //private float gameOwerPeriodTime = 2.5f; 
    //private bool flagGameOwer = false; // флаг, если игрок погибает то true

    //private void Awake()
    //{
    //    /* копируем значение во временную переменную 
    //    (таймер скорости выстрелов игрока тарпедами)*/
    //    periodFireTemp = periodFire; 
    //}
    private void Start()
    {
        /* копируем значение во временную переменную 
        (таймер скорости выстрелов игрока тарпедами)*/
        periodFireTemp = periodFire; // был в Awake()
    }

    // UNITY обновление
    void Update()
    {
        
        //transform.position = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0, 0); // перемещение игрока (контролируется кнопками)
        //TimerFire(); // не пришло ли время стрелять, если пришло, то стреляем.


        // включаем обратный отсчёт после гибели игрока
        //if (flagGameOwer) GameOwerPeriodTime(); 
        TimePeriodFire(); // не пришло ли время стрелять, если пришло, то стреляем.
    }


    /*
    // Каждый заданный период времени стреляем.
    // С этим методом я разобрался плохо.
    void TimerFire()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += periodFire;
            Debug.Log("залп");
            FireRoket(); 
        }
    }
    */

    /*
     //точность до милисекунды
function Update()
{
    timeLeft -= Time.deltaTime;
    if ( timeLeft < 0 )
    {
        //что-то сделать по окончанию времени
    }
}
     */

    //public void GameOwerPeriodTime()
    //{
    //    Debug.Log("GameOwerPeriodTime - run");
    //    gameOwerPeriodTime -= Time.deltaTime;
    //    if (gameOwerPeriodTime <= 0)
    //    {
    //        Debug.Log("Game Ower time end!!!!!!!!!");
    //        Destroy(gameObject);
    //        SceneManager.LoadScene("Menu");
    //    }
    //}

    /* Счётчик временного промежутка между выстрелами игрока торпедой.
    Время ожидания вышло, тогда стреляем. Вызываем в каждом кадре.*/
    public void TimePeriodFire()
    {
        periodFireTemp -= Time.deltaTime;
        if (periodFireTemp <= 0)
        {
            FireRoket(); //стреляем ракетой
            periodFireTemp = periodFire; // возвращаем значение периода стрельбы
        }
    }

    // залп (создание) ракеотой
    public void FireRoket()
    {
        Transform tp = this.GetComponent<Transform>(); // берём Трансформ игрока (себя)
        Instantiate(instRoket, tp.position, tp.rotation); // создаём клон от instRoket, наследуем положене и вращение от игрока (себя) this
    }

    // Обноружение столкновения с врагом. У врагов всё есть, и
    // Box Collider (is Trigger - on) и RigidBody (is Kinematic - on),
    // у игрока только Box Collider.
    private void OnTriggerEnter(Collider other)
    {
        // игрок столкнулся с врагом
        if (other.gameObject.CompareTag("enemy"))
        {

            //app.GetComponent<app>().GameOwer(); //Game Ower в главном коде
            
            // переключаем флаг конца игры в главном коде
            app.GetComponent<app>().flagGameOwer = true;
            Destroy(gameObject); // Убиваем игрока
            
            
            // Скрываем но не удаляем игрока, так-как нужно ещё немного
            // выполнять скрипт.
            //this.GetComponent<MeshRenderer>().enabled = false;
            //this.GetComponent<BoxCollider>().enabled = false;

        }
    }
}
