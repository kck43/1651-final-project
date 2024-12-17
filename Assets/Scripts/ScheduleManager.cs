using UnityEngine;
using System.Timers;
using System;
using System.Threading.Tasks;

public class ScheduleManager : MonoBehaviour
{
    public struct Task
    {
        public Task(int id, int c, int t, int r)
        {
            ID = id;
            ExecutionTime = c;
            Period = t;
            ArrivalTime = r;
        }

        public int ID;
        public int ExecutionTime;
        public int Period;  // Also deadline (relative)
        public int ArrivalTime;
    }

    public int JitterBound = 250;
    private System.Random Generator;
    public int SimulationTime;
    public int JitterDelay;
    public int SimulationDelay = 500;


    public static Task[] TaskSet1;
    public static Task[] TaskSet2;
    // Schedules are from time 0-220, split into lines of 50 instants
    public static int[] Schedule1_RMS = new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, -1, -1, -1, -1, -1, -1, -1, 5, 5, 5, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 7, 7, 7, -1, -1, -1, 1, 1, 1, 1, 1, -1, -1, -1, -1, -1, 5, 5, 5, 5, -1, -1, -1, -1, -1, -1, 3, 3, 3, 3, 3, 3, 3, 6, 6, 6, 6, 6, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static int[] Schedule1_EDF = new[] {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,5,5,5,5,3,3,3,3,3,3,
                                         1,1,1,1,1,3,6,6,6,6,7,7,7,6,6,5,5,5,5,5,5,5,5,-1,-1,-1,-1,-1,-1,-1,5,5,5,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,5,5,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,7,7,7,-1,-1,-1,
                                         1,1,1,1,1,-1,-1,-1,-1,-1,5,5,5,5,-1,-1,-1,-1,-1,-1,6,6,6,6,6,6,3,3,3,3,3,3,3,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         5,5,5,5,1,1,1,1,1,0,7,7,7,0,0,0,0,0,0,0,0};

    public static int[] Schedule2_RMS = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2,2,
                                         1,1,1,2,2,3,3,4,4,4,5,5,6,7,7,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         1,1,1,-1,-1,-1,-1,-1,-1,-1,2,2,2,2,-1,5,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,3,3,
                                         1,1,1,5,5,6,-1,-1,-1,-1,2,2,2,2,-1,-1,-1,-1,-1,-1,-1};
    public static int[] Schedule2_EDF = {1,1,1,0,0,0,0,0,0,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,0,0,0,0,0,0,0,0,
                                         1,1,1,0,0,0,0,3,3,6,4,4,4,7,7,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         1,1,1,-1,-1,-1,-1,-1,-1,-1,2,2,2,2,-1,5,5,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,
                                         1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,0,0,0,0,0,0,0,0,
                                         1,1,1,0,3,3,-1,-1,-1,-1,2,2,2,2,-1,-1,-1,-1,-1,-1,-1};
    private int[] Sched;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Init variables
        SimulationTime = 0;
        Generator = new System.Random();
        Sched = Schedule1_EDF;
    }

    // Update is called once per frame
    async void Update()
    {
        if (Sched[SimulationTime] == 0)
        {
            // Sight task is running
        }
        else
        {
            // Other task is running

            // If the sight task runs next, add jitter
            if (SimulationTime + 1 < Sched.Length && Sched[SimulationTime + 1] == 0)
            {
                JitterDelay = Generator.Next(JitterBound);
                await System.Threading.Tasks.Task.Delay(JitterDelay);
            }
        }

        // Add delay (applied to every task to simulate a time quantum)
        // Base delay of 500ms
        await System.Threading.Tasks.Task.Delay(SimulationDelay);

        SimulationTime++;
    }

    // Initialize the task set where the sight task has 25% utilization (schedulable by both RM and EDF)
    public void InitTaskSet1()
    {
        TaskSet1[0] = new Task(0, 25, 100, 0);      // Sight task
        TaskSet1[1] = new Task(1, 5, 50, 0);
        TaskSet1[2] = new Task(2, 10, 100, 10);
        TaskSet1[3] = new Task(3, 7, 150, 20);
        TaskSet1[4] = new Task(4, 8, 200, 30);
        TaskSet1[5] = new Task(5, 4, 40, 40);
        TaskSet1[6] = new Task(6, 6, 120, 50);
        TaskSet1[7] = new Task(7, 3, 75, 60);
    }

    public void InitTaskSet2()
    {
        TaskSet2[0] = new Task(0, 45, 150, 0);      // Sight task
        TaskSet2[1] = new Task(1, 3, 50, 0);
        TaskSet2[2] = new Task(2, 4, 100, 10);
        TaskSet2[3] = new Task(3, 2, 150, 20);
        TaskSet2[4] = new Task(4, 3, 200, 30);
        TaskSet2[5] = new Task(5, 2, 75, 40);
        TaskSet2[6] = new Task(6, 1, 120, 50);
        TaskSet2[7] = new Task(7, 2, 175, 60);
    }
}
