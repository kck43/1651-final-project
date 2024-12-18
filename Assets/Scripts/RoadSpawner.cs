using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    float offset = 36;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject backWall;
    public GameObject car;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    public void MoveRoad()
    {
        GameObject roadToMove = roads[0];
        roads.Remove(roadToMove);
        float newZ = roads[roads.Count-1].transform.position.z + offset;
        roadToMove.transform.position = new Vector3(0,0,newZ);
        roads.Add(roadToMove);
        rightWall.transform.position += new Vector3(0, 0, 36);
        leftWall.transform.position += new Vector3(0, 0, 36);
        float carX = car.transform.position.x;
        backWall.transform.position = car.transform.position - new Vector3(carX, 0, 10);
    }
}
