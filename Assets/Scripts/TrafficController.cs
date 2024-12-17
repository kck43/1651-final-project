using UnityEngine;

public class TrafficController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float targetTime = 10.0f;
    GameObject red;
    GameObject yellow;
    GameObject green;

    public GameObject over;
    public GameObject blind;
    
    public SightManager sight;
    int color;
    void Start()
    {


        red = GameObject.Find("Red");
        red.SetActive(false);

        yellow = GameObject.Find("Yellow");
        yellow.SetActive(false);

        green = GameObject.Find("Green");
        green.SetActive(false);

        color = 0;


        //over = GameObject.Find("GameOver");
        over.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(color);
        targetTime -= Time.deltaTime;
        if  (targetTime>7.0f){
            green.SetActive(true);
        }

        if (targetTime <=7.0f && color == 0){
            Debug.Log("green go away");
            green.SetActive(false);
            yellow.SetActive(true);
            color = 1;
        }

        else if (targetTime <=5.0f && color == 1){
            yellow.SetActive(false);
            red.SetActive(true);
            color = 2;
        }

        else if (targetTime <= 0 && color == 2){
            red.SetActive(false);
            yellow.SetActive(false);
            green.SetActive(true);
            color = 0;
            targetTime = 10.0f;        
        }
        else{

        }
        
    }


    void Reset(){

    }


    void OnTriggerEnter(Collider other)
    {
        if(color == 2){
            blind.SetActive(false);
            sight.enabled = false;
            over.SetActive(true);
            Debug.Log("Went through a red light!");
        }
    }



}
