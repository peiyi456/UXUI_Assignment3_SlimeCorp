using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] GameObject SlimeTVScreen;
    [SerializeField] Button LevelUpButton = null;
    [SerializeField] Button LevelDownButton = null;
    Camera mainCamera = null;

    Vector3 AttackRoomLocation, FactoryLocation, MarketLocation;
    Vector3[] EachRoomLocation = { new Vector3(2f, 10f, -10f), new Vector3(0f, 0f, -10f), new Vector3(0f, -8.3f, -10f) };
    Vector3 movingLocation;
    public int CameraLocation = 2;
    bool InTransition = false;
    
    public float transitionSpeed = 1f;
    public float zoomingSpeed = 1f;

    private Vector3 touchStart;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = EachRoomLocation[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(InTransition == false)
        {
            if(CameraLocation != 4)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Camera.main.transform.position += direction;
                }
            }

            switch (CameraLocation)
            {
                case 1:
                    {
                        LevelUpButton.interactable = false;
                        LevelDownButton.interactable = true;
                        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, 0f, 13f),
                            10f,
                            transform.position.z);
                        break;
                    }
                case 2:
                    {
                        LevelUpButton.interactable = true;
                        LevelDownButton.interactable = true;
                        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, 0f, 13f),
                            0f,
                            transform.position.z);
                        break;
                    }
                case 3:
                    {
                        LevelUpButton.interactable = true;
                        LevelDownButton.interactable = false;
                        transform.position = new Vector3(
                           Mathf.Clamp(transform.position.x, 0f, 13f),
                           Mathf.Clamp(transform.position.y, -16.4f, -8.3f), 
                           transform.position.z);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        else
        {
            LevelUpButton.interactable = false;
            LevelDownButton.interactable = false;
        }
    }


    public void GoingUp()
    {
        CameraLocation--;
        InTransition = true;
        StartCoroutine(MovingCameraToward(EachRoomLocation[CameraLocation - 1]));
    }

    public void GoingDown()
    {
        CameraLocation++;
        InTransition = true;
        StartCoroutine(MovingCameraToward(EachRoomLocation[CameraLocation - 1]));
    }

    IEnumerator MovingCameraToward(Vector3 destination)
    {
        float elapsedTime = 0;
        while (elapsedTime <= transitionSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, destination, (elapsedTime / transitionSpeed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = destination;
        InTransition = false;
    }
}
