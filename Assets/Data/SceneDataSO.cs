using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Data/SceneData")]
public class SceneDataSO : ScriptableObject
{
    //player
    [Header("Player Data")]
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public Vector3 pivotPosition;
    public Vector3 chaseEnemPos;
    public Vector3 rangeEnemPos;
    public int playerHealth;
}
