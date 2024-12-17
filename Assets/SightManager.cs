using UnityEngine;

public class SightManager : MonoBehaviour
{
    public GameObject blind;
    public GameObject vals;
    int schedule_chosen;
    float jitter_value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blind = GameObject.Find("Blind");
        blind.SetActive(false);

        GameObject valObj = GameObject.Find("Values");
        Values vals = valObj.GetComponent<Values>();
       
        schedule_chosen = vals.getSchedule();
        jitter_value = vals.getJitter();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
