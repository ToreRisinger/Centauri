using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turnNumber = 0;

    public static GameManager instance;
    public static Dictionary<int, Player> players = new Dictionary<int, Player>();
    public static Dictionary<int, CharacterObject> characters = new Dictionary<int, CharacterObject>();
    public static Dictionary<int, BuildingObject> buildings = new Dictionary<int, BuildingObject>();

    private Queue<GameState> gameStateQueue = new Queue<GameState>();
    private List<GameState> gameStateHistory = new List<GameState>();

    public GameObject marinePrefab;
    public GameObject roachPrefab;
    public GameObject commandCenterPrefab;
    public GameObject hivePrefab;

    private Map map;
    private GameObject mapObject;

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
        while (gameStateQueue.Count > 0)
        {
            GameState gameState = gameStateQueue.Dequeue();

            //Handle events
            handleEvents(gameState.events);

            //Update character states
            foreach(CharacterStateData characterStateData in gameState.characters)
            {
                if(!characters.ContainsKey(characterStateData.id))
                {
                    OnCharacterShouldSpawn(characterStateData);
                } else 
                {
                    CharacterObject character = characters[characterStateData.id];
                    
                    character.hp = characterStateData.hp;
                    character.speed = characterStateData.speed;

                    //Only update other players
                    if (players[Client.instance.myId].character != null && players[Client.instance.myId].character.id != characterStateData.id) 
                    {
                        character.direction = characterStateData.direction;
                        character.Move(new Vector2(characterStateData.position.X, characterStateData.position.Y));
                    }
                }
            }

            //Update building states
            foreach (BuildingStateData buildingStateData in gameState.buildings)
            {
                if (!buildings.ContainsKey(buildingStateData.id))
                {
                    onBuildingShouldSpawn(buildingStateData);
                } else
                {
                    //TODO
                }
                   
            }

            //Handle local player input
            if (players.ContainsKey(Client.instance.myId) && players[Client.instance.myId].character != null)
            {
                Player localPlayer = players[Client.instance.myId];
                CharacterObject character = localPlayer.character;
                float delta = Time.deltaTime;
                HashSet<EPlayerAction> actions = PlayerController.GetActions();

                MoveLocalPlayer(character, actions, delta);
                character.SetDirection(actions);
                EObjectDirection direction = character.direction;
                UnityEngine.Vector2 characterPosition = character.transform.position;

                //Send player command to server
                PlayerCommandData cmdData = new PlayerCommandData(turnNumber, delta, new System.Numerics.Vector2(characterPosition.x, characterPosition.y), direction, actions);
                ClientSend.PlayerCommand(cmdData);
            }


            gameStateHistory.Add(gameState);
            turnNumber = gameState.turnNumber;
        }

        if (gameStateHistory.Count > 5)
        {
            gameStateHistory.RemoveAt(0);
        }
    }

    private void MoveLocalPlayer(CharacterObject character, HashSet<EPlayerAction> actions, float _delta)
    {
        System.Numerics.Vector2 currentPosition = new System.Numerics.Vector2(character.transform.position.x, character.transform.position.y);
        System.Numerics.Vector2 newPosition = PlayerMovement.GetNewPosition(currentPosition, actions, _delta, character.speed);
        character.Move(new UnityEngine.Vector2(newPosition.X, newPosition.Y));
    }

    private Dictionary<int, Action<Event>> eventMap = new Dictionary<int, Action<Event>>
    {
        {(int)EventTypes.ServerEvents.PLAYER_DISCONNECTED, (evnt) => { GameManager.instance.onPlayerDisconnect(((PlayerDisconnectedEvent)evnt)); } },
        {(int)EventTypes.ServerEvents.PLAYER_TEAM_CHANGE, (evnt) => { GameManager.instance.onPlayerTeamChange(((PlayerTeamChangeEvent)evnt)); } },
        {(int)EventTypes.ServerEvents.PLAYER_JOINED, (evnt) => {
            PlayerJoinedEvent playerJoinedEvent = (PlayerJoinedEvent)evnt;
            GameManager.instance.onPlayerJoin(playerJoinedEvent.playerId, playerJoinedEvent.username, playerJoinedEvent.teamId);
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
        Debug.Log("onPlayerDisconnect");
        if(players.ContainsKey(evnt.playerDisconnectedId))
        {
            Debug.Log("onPlayerDisconnect1");
            Player player = players[evnt.playerDisconnectedId];
            Debug.Log("Player " + player.username + " (" + player.id + ") disconnected.");
            players.Remove(player.id);
            if(player.character != null && characters.ContainsKey(player.character.id))
            {
                CharacterObject character = characters[player.character.id];
                character.Destroy();
                characters.Remove(character.id);
            }
        }
    }

    public void onPlayerJoin(int _id, string _username, ETeam _teamId)
    {
        if (!players.ContainsKey(_id))
        {
            Debug.Log("Player " + _username + " (" + _id + ") joined the game.");
            Debug.Log("Player " + _username + "(" + _id + ") joined team " + TeamManager.getTeam(_teamId).getTeamName() + ".");

            players.Add(_id, new Player(_id, _username, _teamId));
        } 
    }

    public void onPlayerTeamChange(PlayerTeamChangeEvent evnt)
    {
        //TODO
    }

    public void onBuildingShouldSpawn(BuildingStateData buildingStateData)
    {
        GameObject gameObject;
        switch (buildingStateData.type)
        {
            case EBuildingType.COMMAND_CENTER:
                gameObject = Instantiate(commandCenterPrefab, new Vector2(buildingStateData.position.X, buildingStateData.position.Y), UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
                break;
            case EBuildingType.HIVE:
                gameObject = Instantiate(hivePrefab, new Vector2(buildingStateData.position.X, buildingStateData.position.Y), UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
                break;
            default: return;
        }

        BuildingObject building = gameObject.GetComponent<BuildingObject>();
        //TODO building.
        building.id = buildingStateData.id;
        buildings.Add(building.id, building);
    }

    public void OnCharacterShouldSpawn(CharacterStateData characterStateData)
    {
        if (players.ContainsKey(characterStateData.playerId)) {
            GameObject gameObject;
            Debug.Log(characterStateData.type);
            switch (characterStateData.type)
            {
                case ECharacterType.MARINE:
                    gameObject = Instantiate(marinePrefab, new Vector2(characterStateData.position.X, characterStateData.position.Y), UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
                    break;
                case ECharacterType.ROACH:
                    gameObject = Instantiate(roachPrefab, new Vector2(characterStateData.position.X, characterStateData.position.Y), UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
                    break;
                default: return;
            }

            Player owner = players[characterStateData.playerId];
            CharacterObject character = gameObject.GetComponent<CharacterObject>();
            character.direction = characterStateData.direction;
            character.type = characterStateData.type;
            character.hp = characterStateData.hp;
            character.speed = characterStateData.speed;
            character.id = characterStateData.id;
            character.teamId = characterStateData.teamId;
            owner.character = character;

            if(owner.id == Client.instance.myId)
            {
                character.SetAsLocalPlayer();
            }
            characters.Add(character.id, character);
        }
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
