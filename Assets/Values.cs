using UnityEngine;
//meant to store the two values between scenes
public class Values : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float jitter_value;
    int schedule_chosen;

    public void init(float val, int sch){
        jitter_value = val;
        schedule_chosen = sch;
    }
}
