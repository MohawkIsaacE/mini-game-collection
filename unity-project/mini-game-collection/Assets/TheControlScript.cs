using System.Collections;
using System.Collections.Generic;
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

        public Camera cam;
        public Transform CoinsStorage;
        public Transform PickUpStorage;
        public GameObject MiniGameScoreKeeper;

        [Header("Spawning Variables")]
        public float coinSpawnInterval = 3f;
        public float bombSpawnInterval = 3f;
        public float potionSpawnInterval = 3f;

        public int maxBombs = 4;
        public int maxGoldCoins = 15;
        public int maxSilverCoins = 20;
        public int maxPotions = 2;

        private float coinTimer = 0f;
        private float bombSpawnTimer = 0f;
        private float potionSpawnTimer = 0f;

        void Awake()
        {
            if (cam == null)
                cam = FindObjectOfType<Camera>();
        }

        void Start()
        {
            Debug.Log("Player 1 score: " + MiniGameScoreKeeper.GetComponent<ScoreKeeper>().P1Score);
            Debug.Log("Player 2 score: " + MiniGameScoreKeeper.GetComponent<ScoreKeeper>().P2Score);
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

        void Update()
        {
            coinTimer += Time.deltaTime;
            bombSpawnTimer += Time.deltaTime;
            potionSpawnTimer += Time.deltaTime;

            // Bomb spawn with max limit
            if (bombSpawnTimer >= bombSpawnInterval)
            {
                if (PickUpStorage.childCount < maxBombs)
                    SpawnPickup(Bomb, PickUpStorage, 0.5f);
                bombSpawnTimer = 0f;
            }

            // Potion spawn with max limit
            if (potionSpawnTimer >= potionSpawnInterval)
            {
                int potionCount = 0;
                foreach (Transform child in PickUpStorage)
                {
                    if (child.gameObject.CompareTag("SpeedPotion"))
                        potionCount++;
                }

                if (potionCount < maxPotions)
                    SpawnPickup(SpeedPotion, PickUpStorage, 0.5f);

                potionSpawnTimer = 0f;
            }

            // Coin spawn with max limits
            if (coinTimer >= coinSpawnInterval)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    int goldCount = 0;
                    foreach (Transform child in CoinsStorage)
                    {
                        if (child.gameObject.CompareTag("GoldCoin"))
                            goldCount++;
                    }
                    if (goldCount < maxGoldCoins)
                        SpawnPickup(GoldCoin, CoinsStorage, 0.3f);
                }
                else
                {
                    int silverCount = 0;
                    foreach (Transform child in CoinsStorage)
                    {
                        if (child.gameObject.CompareTag("SilverCoin"))
                            silverCount++;
                    }
                    if (silverCount < maxSilverCoins)
                        SpawnPickup(SilverCoin, CoinsStorage, 0.3f);
                }

                coinTimer = 0f;
            }
        }

        /// <summary>
        /// Spawns a pickup at a valid position that is not overlapping anything else.
        /// </summary>
        void SpawnPickup(GameObject prefab, Transform parent, float checkRadius, int maxAttempts = 20)
        {
            Vector2 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector2 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            for (int i = 0; i < maxAttempts; i++)
            {
                Vector2 randomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
                Collider2D hit = Physics2D.OverlapCircle(randomPos, checkRadius);
                if (hit == null)
                {
                    Instantiate(prefab, randomPos, Quaternion.identity, parent);
                    return;
                }
            }

            Debug.LogWarning($"Could not find valid spawn position for {prefab.name} after {maxAttempts} attempts.");
        }
    }
}
