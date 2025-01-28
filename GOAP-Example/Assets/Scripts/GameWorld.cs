using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameWorld 
{
    private static readonly GameWorld gameWorldInstance = new GameWorld();
    private static WorldStates world;
    private static Queue<GameObject> patientQueue = new Queue<GameObject>();
    static GameWorld()
    {
        world = new WorldStates();
    }

    private GameWorld()
    {

    }

    public void AddPatient(GameObject patient)
    {
        patientQueue.Enqueue(patient);
    }

    public GameObject RemovePatient()
    {
        return patientQueue.Count == 0 ? null : patientQueue.Dequeue();
    }
    public static GameWorld Instance
    {
        get { return gameWorldInstance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
