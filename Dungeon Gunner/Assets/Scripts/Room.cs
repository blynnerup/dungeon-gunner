using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;

    public GameObject[] doors;

    [HideInInspector]
    public bool roomActive;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoors()
    {
        foreach(GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);

            if (closeWhenEntered)
            {
                foreach (var door in doors)
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            roomActive = false;
        }
    }
}
