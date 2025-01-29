using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameWorld 
{
    private static readonly GameWorld _gameWorldInstance = new GameWorld();
    private static WorldStates _world = new WorldStates();
    private static Queue<GameObject> _patientQueue = new Queue<GameObject>();
    private static Queue<GameObject> _cubicleQueue = new Queue<GameObject>();

    static GameWorld()
    {
        GameObject[] cubicles = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (var cubicle in cubicles)
        {
            _cubicleQueue.Enqueue(cubicle);
        }
        //_cubicleQueue = new Queue<GameObject>(GameObject.FindGameObjectsWithTag("Cubicle"));

        if (_cubicleQueue.Any())
        {
            //TODO: replace string with reference to world state scriptable obj
            _world.ModifyState("FreeCubicle", 1);
        }
    }
    private GameWorld()
    {

    }

    public void AddCubicle(GameObject patient)
    {
        _cubicleQueue.Enqueue(patient);
    }

    public GameObject RemoveCubicle()
    {
        return _cubicleQueue.Count == 0 ? null : _cubicleQueue.Dequeue();
    }
    
    public void AddPatient(GameObject patient)
    {
        _patientQueue.Enqueue(patient);
    }

    public GameObject RemovePatient()
    {
        return _patientQueue.Count == 0 ? null : _patientQueue.Dequeue();
    }
    public static GameWorld Instance
    {
        get { return _gameWorldInstance; }
    }

    public WorldStates GetWorld()
    {
        return _world;
    }
}
