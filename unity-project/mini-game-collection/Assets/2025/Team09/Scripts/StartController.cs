using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static MiniGameCollection.ArcadeInput;

namespace MiniGameCollection.Games2025.Team09
{
    public class StartController : MonoBehaviour
    {
        public static StartController Instance { get; private set; }
        public GameObject GameUI;
        public GameObject StartGameUI;
        public GameObject Player1Ready;
        public GameObject Player2Ready;
        public bool GameHasStarted = false;
        public bool player1Ready = false;
        public bool player2Ready = false;
        public GameObject player1Obj; 
        public GameObject player2Obj;

        public GameObject GameCamera;

        [field: SerializeField] MiniGameManager MiniGameManager;
        private void Awake()
        {
            player1Obj = GameObject.Find("2025-team09-player1");
            player2Obj = GameObject.Find("2025-team09-player2");

            // Find the camera object
            GameCamera = GameObject.Find("Pixel Perfect Camera");

            // Randomly choose which map to use
            int whichMap = Random.Range(0, 2);
            // Hard coding my beloved - Isaac
            if (whichMap == 0)
            {
                player1Obj.transform.position = new Vector3(-7, 0, 0);
                player2Obj.transform.position = new Vector3(7, 0, 0);
                GameCamera.transform.position = new Vector3(0, 0, -10);
            }
            else if (whichMap == 1)
            {
                player1Obj.transform.position = new Vector3(-7, -15.89f, 0);
                player2Obj.transform.position = new Vector3(7, -15.89f, 0);
                GameCamera.transform.position = new Vector3(0, -15.89f, -10);
            }

            // Singleton enforcement
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private void Reset()
        {
            GetMiniGameManagerIfNull();
        }

        private void GetMiniGameManagerIfNull()
        {
            if (MiniGameManager == null)
            {
                MiniGameManager = FindAnyObjectByType<MiniGameManager>();
            }
        }

        void Update()
        {
            // Player 1 Press Start / Fire
            if (ArcadeInput.Players[(int)PlayerID.Player1].Action1.Pressed)
            {
                Player1Ready.GetComponent<TextMeshProUGUI>().text = "Player 1: Ready";
                Debug.Log("Player 1 pressed START/FIRE");
                player1Ready = true;
                //StartGame();
            }

            // Player 2 Press Start / Fire
            if (ArcadeInput.Players[(int)PlayerID.Player2].Action1.Pressed)
            {
                player2Ready = true;
                Player2Ready.GetComponent<TextMeshProUGUI>().text = "Player 2: Ready";
                Debug.Log("Player 2 pressed START/FIRE");
                //StartGame();
            }
            if (player1Ready && player2Ready && GameHasStarted == false)
            {
                StartGame();
            }
        }
        private void Start()
        {
            StartGameUI.SetActive(true);
            GameUI.SetActive(false);
        }
        public IEnumerator StunPlayerController(PlayerController pc, float duration)
        {
            // Get SpriteRenderer from the same GameObject as PlayerController
            SpriteRenderer sr = pc.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red; // show stunned

            pc.enabled = false; // stop movement/input
            Rigidbody2D rb = pc.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = Vector2.zero; // freeze movement

            yield return new WaitForSeconds(duration);

            // Restore
            pc.enabled = true;
            if (sr != null)
                sr.color = Color.white; // back to normal
        }
        void StartGame()
        {
            // Start mini game
            GameHasStarted = true;
            MiniGameManager.StartGame();
            StartGameUI.SetActive(false);
            GameUI.SetActive(true);
            
            var player1 = player1Obj.GetComponent<PlayerController>();
            var player2 = player2Obj.GetComponent<PlayerController>();
            StartCoroutine(StunPlayerController(player1, 3f));
            StartCoroutine(StunPlayerController(player2, 3f));
        }
    }
}
