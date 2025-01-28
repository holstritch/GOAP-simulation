using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientSpawner : MonoBehaviour
{
    public GameObject patientPrefab;
    public int initialPatients = 5;
    public int maxPatients = 20; 
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 8f;

    private int _currentPatientCount = 0;
    
    private void Start()
    {
        for (int i = 0; i < initialPatients; i++)
        {
            SpawnPatient();
        }
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        if (_currentPatientCount <= maxPatients)
        {
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            Invoke(nameof(SpawnPatients), spawnDelay);
        }
    }
    private void SpawnPatients()
    {
        SpawnPatient();
        ScheduleNextSpawn();
    }

    private void SpawnPatient()
    {
        GameObject patient = Instantiate(patientPrefab, transform.position, Quaternion.identity);
        patient.transform.SetParent(transform);
        _currentPatientCount++;
    }
}
