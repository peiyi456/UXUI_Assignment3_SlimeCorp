using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingSpawnSlime : MonoBehaviour
{
    public GameObject slimeGameObject;
    public GameObject[] SpawnedLocation;
    public GameObject[] SlimeFactories;
    public Slime[] SlimeType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            for (int i = 0; i < 4; i++)
            {
                if(GameManagerScript.UnlockLab[i] == true)
                {
                    float RandomX = Random.Range(2f, 35f);
                    float RandomY = Random.Range(0.5f, 3f);

                    SlimeFactories[i].GetComponent<Animator>().SetTrigger("Shoot");
                    GameObject spawnedSlime = Instantiate(slimeGameObject, SpawnedLocation[i].transform.position, Quaternion.identity) as GameObject;
                    DecideSlimeType(spawnedSlime, SlimeType[i]);
                    GameManagerScript.SlimeTypeCount[i] +=  1 * GameManagerScript.LabLevel[i];
                    spawnedSlime.GetComponent<Rigidbody2D>().AddForce(new Vector2(RandomX, RandomY), ForceMode2D.Impulse);
                }
            }
        }
    }

    void DecideSlimeType(GameObject slime, Slime slimetype)
    {
        slime.name = slimetype.name;
        slime.GetComponent<Animator>().runtimeAnimatorController = slimetype.animatorController;
        slime.tag = slimetype.tag;
    }
}
