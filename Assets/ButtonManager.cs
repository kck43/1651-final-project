using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    static int taskset;
    static int algorithm;
    static float jitter_value;
    //public Slider slider;
    //Scenemanager scenemanager;
    AsyncOperation sceneAsync;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskset = 0; //no mode chosen
        algorithm = -1;
        jitter_value = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ChangeScene()
    {
        // On play/start, transfer Values GameObject to the Level scene
        GameObject vals = GameObject.Find("Values");
        vals.GetComponent<Values>().init(jitter_value, taskset, algorithm);
        DontDestroyOnLoad(vals);

        // Load Level behind the scenes
        AsyncOperation scene = SceneManager.LoadSceneAsync("Level", LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        // Wait until scene is loaded
        while (scene.progress < 0.9f)
        {
            Debug.Log("Loading Level. Progress: " + scene.progress);
            yield return null;
        }

        // Set Level Scene to be active
        sceneAsync.allowSceneActivation = true;
        Scene level = SceneManager.GetSceneByName("Level");
        if (level.IsValid())
        {
            Debug.Log("Level scene is valid.");
            SceneManager.MoveGameObjectToScene(vals, level);
            SceneManager.SetActiveScene(level);
        }
        Debug.Log("Level scene loaded and Values transferred");
    }

    public void Test(){
        Debug.Log("D M > ");
    }

    public void SelectEDF(){ //1
        Debug.Log(algorithm);
        GameObject EDFbutton = GameObject.Find("EDF");
        EDFbutton.GetComponent<Image>().color = new Color(255,0,255);
        //if another already chosen - switch it off
        if (algorithm == 1){
            GameObject RMbutton = GameObject.Find("RM");
            RMbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        algorithm = 0;
        
    }

    public void SelectRM(){ //2
        Debug.Log(algorithm);

        GameObject RMbutton = GameObject.Find("RM");
        RMbutton.GetComponent<Image>().color = new Color(255,0,255);
        
        //if another already chosen - switch it off
        if (algorithm == 0){
            GameObject EDFbutton = GameObject.Find("EDF");
            EDFbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        algorithm = 1;
    }

    //called every time slider is interacted with
    public void JitterSliderGet(){
        Slider jitter = (Slider)FindObjectOfType(typeof(Slider));
        jitter_value = jitter.value;
    }
}
