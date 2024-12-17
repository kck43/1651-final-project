using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    static int schedule_chosen;
    static float jitter_value;
    //public Slider slider;
    //Scenemanager scenemanager;
    public Values vals;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        schedule_chosen = 0; //no mode chosen
        jitter_value = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        //Values vals = GameObject.Find("Values");
        vals.init(jitter_value, schedule_chosen);
        DontDestroyOnLoad(vals);
        SceneManager.LoadScene("Level");
    }

    public void Test(){
        Debug.Log("D M > ");
    }

    public void SelectEDF(){ //1
        Debug.Log(schedule_chosen);
        GameObject EDFbutton = GameObject.Find("EDF");
        EDFbutton.GetComponent<Image>().color = new Color(255,0,255);
        //if another already chosen - switch it off
        if (schedule_chosen == 2){
            GameObject RMbutton = GameObject.Find("RM");
            RMbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        if (schedule_chosen == 3){
            GameObject DMbutton = GameObject.Find("DM");
            DMbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        schedule_chosen = 1;
        
    }

    public void SelectRM(){ //2
        Debug.Log(schedule_chosen);

        GameObject RMbutton = GameObject.Find("RM");
        RMbutton.GetComponent<Image>().color = new Color(255,0,255);
        
        //if another already chosen - switch it off
        if (schedule_chosen == 1){
            GameObject EDFbutton = GameObject.Find("EDF");
            EDFbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        if (schedule_chosen == 3){
            GameObject DMbutton = GameObject.Find("DM");
            DMbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        schedule_chosen = 2;
    }

    public void SelectDM(){ //3
        Debug.Log(schedule_chosen);

        GameObject DMbutton = GameObject.Find("DM");
        DMbutton.GetComponent<Image>().color = new Color(255,0,255);
        if (schedule_chosen == 1){
            GameObject EDFbutton = GameObject.Find("EDF");
            EDFbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        if (schedule_chosen == 2){
            GameObject RMbutton = GameObject.Find("RM");
            RMbutton.GetComponent<Image>().color = new Color(255,255,255);
        }
        schedule_chosen = 3;
        

    }

    //called every time slider is interacted with
    public void JitterSliderGet(){
        Slider jitter = (Slider)FindObjectOfType(typeof(Slider));
        jitter_value = jitter.value;
    }
}
