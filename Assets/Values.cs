using UnityEngine;
//meant to store the two values between scenes
public class Values : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float jitter_value;
    int schedule_chosen;

    public void init(float val, int sch){
        Debug.Log("init");
        Debug.Log(val);
        Debug.Log(sch);

        jitter_value = val;
        schedule_chosen = sch;
    }

    public float getJitter(){
        return jitter_value;
    }

    public int getSchedule(){
        return schedule_chosen;
    }
}
