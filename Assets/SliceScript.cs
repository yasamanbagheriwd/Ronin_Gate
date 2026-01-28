using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SliceScript : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemyType enemyType;

    [Header("Direction Slice Settings")]
    public float directionMin = 0f;
    public float directionMax = 22.5f;

    [Header("References")]
    public Transform target;




    public bool IsInSlice(float angle)
    {
        return angle >= directionMin && angle <= directionMax;
    }

}
