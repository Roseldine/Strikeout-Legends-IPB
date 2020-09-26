using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager Instance;
    private void Awake()
    {
        Instance = GetComponent<GameManager>();
    }

}
