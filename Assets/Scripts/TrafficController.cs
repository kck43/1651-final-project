using UnityEngine;

public class TrafficController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float targetTime = 10.0f;
    GameObject red;
    GameObject yellow;
    GameObject green;

    int color;
    void Start()
    {
        red = GameObject.Find("Red");
        red.SetActive(false);

        yellow = GameObject.Find("Yellow");
        yellow.SetActive(false);

        green = GameObject.Find("Green");
        green.SetActive(true);

        color = 0;

    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <=7.0f && color == 0){
            green.SetActive(false);
            yellow.SetActive(true);
            color = 1;
        }

        else if (targetTime <=5 && color == 1){
            yellow.SetActive(false);
            red.SetActive(true);
        }
        else{

        }
    }


    void Reset(){
        red.SetActive(false);
        yellow.SetActive(false);
        green.SetActive(true);
        color = 0;
    }


    void OnTriggerEnter(Collider other)
    {
        if(color == 1){
            Debug.Log("Went through a red light!");
        }
    }



}
