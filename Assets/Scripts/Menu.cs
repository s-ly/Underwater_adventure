using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //загрузка сцены

// Управление Меню и подменю игры.
public class Menu : MonoBehaviour
{
    GameObject app; // ссылка на главный код (нельзя прикрепить в ручную, надо кодом)

    // при старте
    private void Start()
    {
        //app = GameObject.FindWithTag("app"); // ссылка на главный код app (так как нельзя руками)        
    }

    //загрузка меню выбора уровня
    public void Play()
    {
        SceneManager.LoadScene("SelectLevelMenu");
    }
    //выход из игры (вызовится кнопкой в меню)
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }
    //назад в главное меню
    public void BackMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //загрузка уровня 1
    public void PlayLevel_01()
    {
        Data.staticDataGameLevel = 1; // заносим в staticData какой уровень выбрал игрок
        SceneManager.LoadScene("Level");
    }
    //загрузка уровня 2
    public void PlayLevel_02()
    {
        Data.staticDataGameLevel = 2; // заносим в staticData какой уровень выбрал игрок
        SceneManager.LoadScene("Level");
    }
    //загрузка уровня 3
    public void PlayLevel_03()
    {
        Data.staticDataGameLevel = 3; // заносим в staticData какой уровень выбрал игрок
        SceneManager.LoadScene("Level");
    }
    //загрузка уровня 4
    public void PlayLevel_04()
    {
        Data.staticDataGameLevel = 4; // заносим в staticData какой уровень выбрал игрок
        SceneManager.LoadScene("Level");
    }
}
