using Shared;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turnNumber = 0;

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static TeamManager teamManager = new TeamManager();

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

    #region FixedUpdate
    private void FixedUpdate()
    {
        //TODO now we apply directly -> fix add interpolation between old and new
        while(gameStateQueue.Count > 0)
        {
            GameState gameState = gameStateQueue.Dequeue();

            //Handle events
            handleEvents(gameState.events);

            //Update player states
            HashSet<int> foundPlayers = new HashSet<int>();
            for(int i = 0; i < gameState.players.Count; i++)
            {
                PlayerStateData playerStateData = gameState.players[i];
                foundPlayers.Add(playerStateData.id);
                if (players.ContainsKey(playerStateData.id))
                {
                    if (playerStateData.id != Client.instance.myId)
                    {
                        players[playerStateData.id].Move(playerStateData.position);
                        players[playerStateData.id].direction = playerStateData.direction;
                    }
                    players[playerStateData.id].teamId = playerStateData.teamId;
                }
            }

            //Remove players that are no longer updated
            List<int> playersToRemove = new List<int>();
            foreach(PlayerManager playerManager in players.Values)
            {
                if(!foundPlayers.Contains(playerManager.id))
                {
                    playersToRemove.Add(playerManager.id);
                }
            }

            foreach (int playerIdToRemove in playersToRemove)
            {
                RemovePlayer(playerIdToRemove);
            }
            
            gameStateHistory.Add(gameState);
            turnNumber = gameState.turnNumber;
        }

        if(gameStateHistory.Count > 5)
        {
            gameStateHistory.RemoveAt(0);
        }
        
        teamManager.centauriTeam.clearTeam();
        teamManager.marineTeam.clearTeam();
        teamManager.spectatorTeam.clearTeam();

        //Set teams
        foreach (PlayerManager p in players.Values)
        {
            teamManager.getTeam(p.teamId).AddPlayer(p.id);
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

    private void MoveLocalPlayer(PlayerManager _localPlayer, HashSet<EPlayerAction> actions, float _delta)
    {
        System.Numerics.Vector2 currentPosition = new System.Numerics.Vector2(_localPlayer.transform.position.x, _localPlayer.transform.position.y);
        System.Numerics.Vector2 newPosition = PlayerMovement.GetNewPosition(currentPosition, actions, _delta);
        _localPlayer.MoveVelocity(new UnityEngine.Vector2(newPosition.X, newPosition.Y));
    }

    private void RemovePlayer(int id)
    {
        if (players.ContainsKey(id))
        {
            players[id].Destroy();
            players.Remove(id);
        }
    }

    private Dictionary<int, Action<Event>> eventMap = new Dictionary<int, Action<Event>>
    {
        {(int)EventTypes.ServerEvents.PLAYER_DISCONNECTED, (evnt) => { GameManager.instance.onPlayerDisconnect(((PlayerDisconnectedEvent)evnt)); } },
        {(int)EventTypes.ServerEvents.PLAYER_TEAM_CHANGE, (evnt) => { GameManager.instance.onPlayerTeamChange(((PlayerTeamChangeEvent)evnt)); } },
        {(int)EventTypes.ServerEvents.PLAYER_JOINED, (evnt) => {
            PlayerJoinedEvent playerJoinedEvent = (PlayerJoinedEvent)evnt;
            System.Numerics.Vector2 spawnPos = playerJoinedEvent.spawnPosition;
            Vector2 spawnPosition = new Vector2(spawnPos.X, spawnPos.Y);
            GameManager.instance.onPlayerJoin(playerJoinedEvent.playerId, playerJoinedEvent.username, playerJoinedEvent.teamId, spawnPosition);
        }},

    };

    private void handleEvents(Queue<Event> events)
    {
        while(events.Count > 0)
        {
            Event evnt = events.Dequeue();
            eventMap[(int)evnt.eventType](evnt);
        }
    }

    #endregion

    #region events

    private void onPlayerDisconnect(PlayerDisconnectedEvent evnt)
    {
        if(players.ContainsKey(evnt.playerDisconnectedId))
        {
            PlayerManager player = players[evnt.playerDisconnectedId];
            Debug.Log("Player " + player.name + " (" + player.id + ") disconnected.");
            RemovePlayer(player.id);
        }  
    }

    public void onPlayerJoin(int _id, string _username, ETeam _teamId, UnityEngine.Vector2 _position)
    {
        if (!players.ContainsKey(_id))
        {
            Debug.Log("Player " + _username + " (" + _id + ") joined the game.");
            Debug.Log("Player " + _username + "(" + _id + ") joined team " + teamManager.getTeam(_teamId).getTeamName() + ".");

        
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
            _player.GetComponent<PlayerManager>().teamId = _teamId;
            players.Add(_id, _player.GetComponent<PlayerManager>());
        } 
    }

    public void onPlayerTeamChange(PlayerTeamChangeEvent evnt)
    {
        //TODO
    }

    #endregion

    #region servercalls

    public void Initialize(int _mapId)
    {
        map = MapManager.instance.GetMap(_mapId);

        GameObject _mapPrefab = MapManager.instance.GetMapPrefab(_mapId);
        System.Numerics.Vector2 _mapPosition = Map.mapPosition;
        mapObject = Instantiate(_mapPrefab, new UnityEngine.Vector2(_mapPosition.X, _mapPosition.Y), UnityEngine.Quaternion.identity);
    }

    public void pushGameState(GameState _gameState)
    {
        gameStateQueue.Enqueue(_gameState);
    }

    #endregion
}
