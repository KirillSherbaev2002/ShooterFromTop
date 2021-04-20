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
            if (Zombies.Length <= currentLenght && currentLenght <= NeedsToBeKilled)
            {
                UpdateZombie();
            }
        }
    }

    public void SceneReload()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; 
    }
}
