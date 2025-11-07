using TMPro;
using UnityEngine;

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

        [field: SerializeField] MiniGameManager MiniGameManager;
        private void Awake()
        {
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
        void StartGame()
        {
            // Start mini game
            GameHasStarted = true;
            MiniGameManager.StartGame();
            StartGameUI.SetActive(false);
            GameUI.SetActive(true);
        }
    }
}
