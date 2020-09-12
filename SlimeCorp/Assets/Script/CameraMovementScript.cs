using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField] GameObject SlimeTVScreen;
    Camera mainCamera = null;
    [SerializeField] Camera AttackSceneCamera = null;

    Vector3 AttackRoomLocation, FactoryLocation, MarketLocation;
    Vector3 movingLocation;
    public int CameraLocation = 2;
    bool InTransition = false;
    float elapseTime = 0;
    
    public float transitionSpeed = 4f;
    public float zoomingSpeed = 1f;

    private Vector3 touchStart;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        AttackRoomLocation = new Vector3(0f, 0f, -10f);
        FactoryLocation = new Vector3(0f, -8.3f, -10f);
        MarketLocation = new Vector3(0f, 10f, -10f);

        transform.position = AttackRoomLocation;
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
                        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, 0f, 13f),
                            10f,
                            transform.position.z);
                        break;
                    }
                case 2:
                    {
                        transform.position = new Vector3(
                            Mathf.Clamp(transform.position.x, 0f, 13f),
                            0f,
                            transform.position.z);
                        break;
                    }
                case 3:
                    {
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
    }

    void LateUpdate()
    {
        if(InTransition == true)
        {
            elapseTime += Time.deltaTime;
            if (transform.position != movingLocation)
            {
                transform.position = Vector3.Lerp(transform.position, movingLocation, (elapseTime / transitionSpeed));
            }
            else
            {
                InTransition = false;
            }
        }
    }

    public void TowardMarket()
    {
        if(CameraLocation != 1)
        {
            movingLocation = new Vector3(transform.position.x, MarketLocation.y, MarketLocation.z);
            InTransition = true;
            CameraLocation = 1;
            elapseTime = 0;
        }
    }

    public void TowardAttackRoom()
    {
        if (CameraLocation != 2)
        {
            movingLocation = new Vector3(transform.position.x, AttackRoomLocation.y, AttackRoomLocation.z); ;
            InTransition = true;
            CameraLocation = 2;
            elapseTime = 0;
        }
    }

    public void TowardFactory()
    {
        if (CameraLocation != 3)
        {
            movingLocation = new Vector3(transform.position.x, FactoryLocation.y, FactoryLocation.z); ;
            InTransition = true;
            CameraLocation = 3;
            elapseTime = 0;
        }
    }
}
