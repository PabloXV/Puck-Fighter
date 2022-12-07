using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] public List<Sprite> icons;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    public static EntitySpawner instance;

    private GameObject playerInstance;
    
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        TurnManager.instance.entities.Add(player);
        player.GetComponent<PuckController>().team = GameManager.Team.Player;
        player.GetComponent<PuckController>().pd = new PuckData(1.2f, 2f, 25f, 0);

        for (int i = 0; i < 4; i++)
        {
            Vector2 position = new Vector2(Random.Range(-18, 18), Random.Range(-8, 8));
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            TurnManager.instance.entities.Add(enemy);
            enemy.GetComponent<PuckController>().team = GameManager.Team.Enemy;
            enemy.GetComponent<PuckController>().pd = new PuckData();
        }
    }
}
