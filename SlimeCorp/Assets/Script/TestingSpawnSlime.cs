using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestingSpawnSlime : MonoBehaviour
{
    [Header("Access GameObject")]
    public GameObject slimeGameObject;
    public GameObject[] SpawnedLocation;
    public GameObject[] SlimeFactories;
    public Slime[] SlimeType;
    public GameObject[] LabArray;
    public GameObject[] AtkRoomArray;
    public GameObject[] AtkRoomSpawnLocation;
    public PlayableDirector timeline;

    [Header("Balancing Use")]
    int[,] SlimeCount_lab_attackRoom = new int[,] { { 0, 0 }, { 25, 5 }, { 30, 10 }, { 50, 20 }, { 100, 30 }, { 200, 40 } };
    float cooldownSpawnTime = 0;

    void Start()
    {
        InvokeRepeating("CheckAttackRoomSlimeAmount", 0, 2);
    }

    void Update()
    {
        
        cooldownSpawnTime += Time.deltaTime;
    }

    void OnMouseDown()
    {
        if(timeline.state == PlayState.Paused)
        {
            for (int i = 0; i < 4; i++)
            {
                if (GameManagerScript.UnlockLab[i] == true)
                {
                    SpawnSlime(i, SpawnedLocation[i].transform.position, true);
                }
            }
        }
    }

    void SpawnSlime(int i, Vector3 SpawnPosition, bool SpawnAtLab)
    {
        SlimeFactories[i].GetComponent<Animator>().SetTrigger("Shoot");
        GameObject spawnedSlime = Instantiate(slimeGameObject, SpawnPosition, Quaternion.identity) as GameObject;
        DecideSlimeType(spawnedSlime, SlimeType[i]);

        if(SpawnAtLab)
        {
            float RandomX = Random.Range(2f, 35f);
            float RandomY = Random.Range(0.5f, 3f);
            spawnedSlime.transform.parent = LabArray[i].transform;
            GameManagerScript.SlimeTypeCount[i] += 1 * GameManagerScript.LabLevel[i];
            spawnedSlime.GetComponent<Rigidbody2D>().AddForce(new Vector2(RandomX, RandomY), ForceMode2D.Impulse);
        }
        else
        {
            spawnedSlime.transform.parent = AtkRoomArray[i].transform;
            GameManagerScript.SlimeTypeForAttackRoom[i]++;
        }
    }

    void DecideSlimeType(GameObject slime, Slime slimetype)
    {
        slime.name = slimetype.name;
        slime.GetComponent<Animator>().runtimeAnimatorController = slimetype.animatorController;
        slime.tag = slimetype.tag;
    }

    void CheckAttackRoomSlimeAmount()
    {
        int[] SlimeShouldBeInAtkRoom = new int[4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (GameManagerScript.SlimeTypeCount[i] >= SlimeCount_lab_attackRoom[j,0] && GameManagerScript.SlimeTypeCount[i] < SlimeCount_lab_attackRoom[j+1, 0])
                {
                    SlimeShouldBeInAtkRoom[i] = SlimeCount_lab_attackRoom[j, 1];
                }
                else if(GameManagerScript.SlimeTypeCount[i] >= SlimeCount_lab_attackRoom[5, 0])
                {
                    SlimeShouldBeInAtkRoom[i] = SlimeCount_lab_attackRoom[5, 1];
                }
            }

            if (GameManagerScript.SlimeTypeForAttackRoom[i] < SlimeShouldBeInAtkRoom[i])
            {
                int randomIndex = 0;
                if (Random.value < 0.5f)
                {
                    randomIndex = 0;
                }
                else
                {
                    randomIndex = 1;
                }
                SpawnSlime(i, AtkRoomSpawnLocation[randomIndex].transform.position, false);
            }
            else if (GameManagerScript.SlimeTypeForAttackRoom[i] > SlimeShouldBeInAtkRoom[i])
            {
                int needToDelete = GameManagerScript.SlimeTypeForAttackRoom[i] - SlimeShouldBeInAtkRoom[i];
                DeleteAtkRoomSlime(needToDelete, i);
            }
        }
    }

    void DeleteAtkRoomSlime(int amountDelete, int index)
    {
        for (int i = 0; i < amountDelete && i < AtkRoomArray[index].transform.childCount; i++)
        {
            Destroy(AtkRoomArray[index].transform.GetChild(i).gameObject);
        }
        GameManagerScript.SlimeTypeForAttackRoom[index] -= amountDelete;
        if(GameManagerScript.SlimeTypeForAttackRoom[index] < 0)
        {
            GameManagerScript.SlimeTypeForAttackRoom[index] = 0;
        }
    }
}
