using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePref;
    public GameObject SpawnEffect;
    public int currentLenght;
    [SerializeField] int time;
    public PlayerMove player;

    public GameObject YouWon;
    public int NeedsToBeKilled;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        currentLenght = 0;
    }
    public void UpdateZombie()
    {
        var Zombies = FindObjectsOfType<Zombie>();

        if (Zombies.Length <= currentLenght && currentLenght <= NeedsToBeKilled)
        {
            SpawnZombie();
            if (Zombies.Length <= currentLenght && currentLenght <= NeedsToBeKilled)
            {
                UpdateZombie();
            }
        }
    }

    void SpawnZombie()
    {
        var SpawnPlace = new Vector3(player.transform.position.x + Random.Range(15, -15), player.transform.position.y + Random.Range(15, -15), -5);
        Instantiate(ZombiePref, SpawnPlace, transform.rotation);
        Instantiate(SpawnEffect, SpawnPlace, transform.rotation);
    }

    public void SceneReload()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; 
    }
}
