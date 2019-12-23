using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;
    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left};
    public Direction selectedDirection;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
