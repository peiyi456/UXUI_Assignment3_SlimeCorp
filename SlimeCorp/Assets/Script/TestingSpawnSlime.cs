using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Balancing Use")]
    int[,] SlimeCount_lab_attackRoom = new int[,] { { 0, 0 }, { 25, 5 }, { 30, 10 }, { 50, 20 }, { 100, 30 }, { 200, 40 } };
    int cooldownDeleteTime = 0;

    void Start()
    {
        InvokeRepeating("CheckAttackRoomSlimeAmount", 2, 2);
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManagerScript.UnlockLab[i] == true)
            {
                SpawnSlime(i, SpawnedLocation[i].transform.position, true);
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
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (GameManagerScript.SlimeTypeCount[i] >= SlimeCount_lab_attackRoom[j,0] && GameManagerScript.SlimeTypeCount[i] < SlimeCount_lab_attackRoom[j+1, 0])
                {
                    if (GameManagerScript.SlimeTypeForAttackRoom[i] < SlimeCount_lab_attackRoom[j,1])
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
                    else if (GameManagerScript.SlimeTypeForAttackRoom[i] > SlimeCount_lab_attackRoom[j, 1] && cooldownDeleteTime == 0)
                    {
                        cooldownDeleteTime = GameManagerScript.SlimeTypeForAttackRoom[i] - SlimeCount_lab_attackRoom[j, 1];
                        StartCoroutine(DeleteAtkRoomSlime(cooldownDeleteTime, i));
                    }
                }
            }
        }
    }

    IEnumerator DeleteAtkRoomSlime(int amountDelete, int index)
    {
        while(amountDelete != 0)
        {
            int randomNum = Random.Range(0, AtkRoomArray[index].transform.childCount);
            Destroy(AtkRoomArray[index].transform.GetChild(randomNum).gameObject);
            GameManagerScript.SlimeTypeForAttackRoom[index]--;
            amountDelete--;
            yield return null;
        }
        Debug.Log("YESSSS");
    }
}
