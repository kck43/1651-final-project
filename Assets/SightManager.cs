using UnityEngine;

public class SightManager : MonoBehaviour
{
    public GameObject blind;
    public GameObject vals;
    int schedule_chosen;
    float jitter_value;
    float timer;
    float period;
    float nextActionTime;
    int GameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0f;
        period = 1.0f;
        nextActionTime = 0;

        blind = GameObject.Find("Blind");
        blind.SetActive(false);

        GameObject valObj = GameObject.Find("Values");
        Values vals = valObj.GetComponent<Values>();
       
        schedule_chosen = vals.getSchedule();
        
        jitter_value = vals.getJitter();

        if (jitter_value == 0 ){
            jitter_value += 1;
        }
        else{
            jitter_value = 100/jitter_value;

        }
        

    }

    // Update is called once per frame


void Update () {
    timer += Time.deltaTime;
    Debug.Log("time " + timer + "Atime " + nextActionTime, this);

    //Debug.Log("tim" + timer);
    if (timer >= nextActionTime) {
        float jitter = Random.Range(0, jitter_value);
       nextActionTime = nextActionTime + period + jitter;
       if(blind.activeSelf){
            blind.SetActive(false);
       }
       else{
            blind.SetActive(true);
       }
    }

}
}
