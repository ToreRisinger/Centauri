using PlayerDirection;
using Shared;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public int turnNumber = 0;

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    private Queue<GameState> gameStateQueue = new Queue<GameState>();
    private List<GameState> gameStateHistory = new List<GameState>();
    
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    public Map map;
    public GameObject mapObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
        //TODO now we apply directly -> fix add interpolation between old and new
        while(gameStateQueue.Count > 0)
        {
            GameState gameState = gameStateQueue.Dequeue();
            for(int i = 0; i < gameState.players.Count; i++)
            {
                PlayerStateData playerStateData = gameState.players[i];
                if(players.ContainsKey(playerStateData.id) && playerStateData.id != Client.instance.myId)
                {
                    players[playerStateData.id].Move(playerStateData.position);
                    players[playerStateData.id].direction = playerStateData.direction;
                }
            }

            gameStateHistory.Add(gameState);
            turnNumber = gameState.turnNumber;
        }

        if(gameStateHistory.Count > 5)
        {
            gameStateHistory.RemoveAt(0);
        }


        //Move local player
        if (players.ContainsKey(Client.instance.myId))
        {
            PlayerManager localPlayer = players[Client.instance.myId];
            float delta = Time.deltaTime;
            HashSet<EPlayerAction> actions = PlayerController.GetActions();

            MoveLocalPlayer(localPlayer, actions, delta);
            localPlayer.SetDirection(actions);
            EPlayerDirection direction = localPlayer.direction;
            UnityEngine.Vector2 localPlayerPosition = localPlayer.transform.position;

            //Send player command to server
            ClientSend.PlayerCommand(actions, localPlayerPosition, direction, turnNumber, delta);
        }
    }

    public void Initialize(int _mapId)
    {
        map = MapManager.instance.GetMap(_mapId);

        GameObject _mapPrefab = MapManager.instance.GetMapPrefab(_mapId);
        System.Numerics.Vector2 _mapPosition = Map.mapPosition;
        mapObject = Instantiate(_mapPrefab, new UnityEngine.Vector2(_mapPosition.X, _mapPosition.Y), UnityEngine.Quaternion.identity);
    }

    public void SpawnPlayer(int _id, string _username, UnityEngine.Vector2 _position)
    {
        if(!players.ContainsKey(_id))
        {
            GameObject _player;
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(localPlayerPrefab, _position, UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
            }
            else
            {
                _player = Instantiate(playerPrefab, _position, UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
            }

            _player.GetComponent<PlayerManager>().id = _id;
            _player.GetComponent<PlayerManager>().username = _username;
            players.Add(_id, _player.GetComponent<PlayerManager>());
        } 
    }

    public void pushGameState(GameState _gameState)
    {
        gameStateQueue.Enqueue(_gameState);
    }

    public void MoveLocalPlayer(PlayerManager _localPlayer, HashSet<EPlayerAction> actions, float _delta)
    {
        System.Numerics.Vector2 currentPosition = new System.Numerics.Vector2(_localPlayer.transform.position.x, _localPlayer.transform.position.y);
        System.Numerics.Vector2 newPosition = PlayerMovement.GetNewPosition(currentPosition, actions, _delta);
        _localPlayer.MoveVelocity(new UnityEngine.Vector2(newPosition.X, newPosition.Y));
    }
}
