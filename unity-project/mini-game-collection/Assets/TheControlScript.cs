using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace MiniGameCollection.Games2025.Team09
{
    public class TheControlScript : MonoBehaviour
    {
        [Header("Coins")]
        public GameObject SilverCoin;
        public GameObject GoldCoin;

        [Header("Pickups")]
        public GameObject Bomb;
        public GameObject SpeedPotion;

        public Camera cam; // drag the camera here

        public Transform CoinsStorage;
        public Transform PickUpStorage;
        public GameObject MiniGameScoreKeeper;
        [Header("Spawning Variables")]
        float coinTimer = 0f;
        float bombSpawnTimer = 0f;
        float potionSpawnTimer = 0f;
        public float coinSpawnInterval = 3f;
        public float bombSpawnInterval = 3f;
        public float potionSpawnInterval = 3f;
        public int goldSpawnAmount = 15;
        public int silverSpawnAmount = 15;
        // Start is called before the first frame update
        void Start()
        {
            //Score
            //MiniGameScoreUI
            Debug.Log("Player 1 score: " + MiniGameScoreKeeper.GetComponent<ScoreKeeper>().P1Score);
            Debug.Log("Player 2 score: " + MiniGameScoreKeeper.GetComponent<ScoreKeeper>().P2Score);
        }
        void Awake()
        {
            // If not assigned in inspector, auto-find any Camera in the scene
            if (cam == null)
            {
                cam = FindObjectOfType<Camera>();
            }
        }
        public void GivePlayer1APoint(int rewardAmount)
        {
            MiniGameScoreKeeper.GetComponent<ScoreKeeper>().AddScore(PlayerID.Player1, rewardAmount);
            Debug.Log("Player 1 stats increased!");
        }
        public void GivePlayer2APoint(int rewardAmount)
        {
            MiniGameScoreKeeper.GetComponent<ScoreKeeper>().AddScore(PlayerID.Player2, rewardAmount);
            Debug.Log("Player 2 stats increased!");
        }
        public void SpawnSilverCoin()
        {
            Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector2 randomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            Instantiate(SilverCoin, randomPos, Quaternion.identity, CoinsStorage);
        }
        public void SpawnGoldCoin()
        {
            Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector2 randomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            Instantiate(GoldCoin, randomPos, Quaternion.identity, CoinsStorage);
        }
        public void SpawnABomb()
        {
            Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector2 randomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            Instantiate(Bomb, randomPos, Quaternion.identity, PickUpStorage);
        }
        public void SpawnASpeedPotion()
        {
            Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            Vector2 randomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            Instantiate(SpeedPotion, randomPos, Quaternion.identity, PickUpStorage);
        }
        // Update is called once per frame
        void Update()
        {
            coinTimer += Time.deltaTime;
            bombSpawnTimer += Time.deltaTime;
            potionSpawnTimer += Time.deltaTime;
            if (bombSpawnTimer >= bombSpawnInterval) // Every 5 seconds
            {
                SpawnABomb();
                bombSpawnTimer = 0;
            }
            if (potionSpawnTimer >= potionSpawnInterval) // Every 5 seconds
            {
                SpawnASpeedPotion();
                potionSpawnTimer = 0;
            }
            if (coinTimer >= coinSpawnInterval) // Every 5 seconds
            {
                int goldOrSilver = Random.Range(1, 3);
                if (goldOrSilver == 1)
                {
                    SpawnGoldCoin();
                }
                else
                {
                    SpawnSilverCoin();
                }
                coinTimer = 0f;
            }
        }
    }
}
