﻿
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;

namespace Server
{
    class GameLogic
    {
        private static Map map;
        private static Dictionary<int, Player> players;
        private static Dictionary<int, BuildingObject> buildings;
        public static Dictionary<int, CharacterObject> characters;
        private static Queue<PlayerCommandData> playerCommands;
        private static Queue<Event> events;
        private static int turnNumber;

        public static void init()
        {
            map = new TestMap();
            players = new Dictionary<int, Player>();
            buildings = new Dictionary<int, BuildingObject>();
            characters = new Dictionary<int, CharacterObject>();
            events = new Queue<Event>();
            playerCommands = new Queue<PlayerCommandData>();
            turnNumber = 0;

            BuildingObject commandCenter = new CommandCenterObject(Map.mapPosition + map.getMarineSpawnPoint());
            BuildingObject hive = new HiveObject(Map.mapPosition + map.getAlienSpawnPoint());
            buildings.Add(commandCenter.id, commandCenter);
            buildings.Add(hive.id, hive);
        }

        public static void Update()
        {
            turnNumber++;

            ThreadManager.UpdateMain();

            handlePlayerCommands();

            foreach(CharacterObject character in characters.Values)
            {
                character.Update();
            }

            foreach (BuildingObject building in buildings.Values)
            {
                building.Update();
            }

            SendGameState(turnNumber);

        }

        public static void handlePlayerCommands()
        {
            while(playerCommands.Count > 0)
            {
                PlayerCommandData playerCommandData = playerCommands.Dequeue();
                
                if(players.ContainsKey(playerCommandData.playerId) && players[playerCommandData.playerId].gameObj != null)
                {
                    Player player = players[playerCommandData.playerId];
                    CharacterObject characterObject = player.gameObj;
                    characterObject.position = playerCommandData.position;
                    characterObject.direction = playerCommandData.direction;

                    handlePlayerAbilities(characterObject, playerCommandData.abilityActivations);


                }
            }
        }

        private static void handlePlayerAbilities(CharacterObject characterObject, List<AbilityActivationData> abilityActivations)
        {
            foreach(AbilityActivationData abilityActivationData in abilityActivations)
            {
                activateAbility(characterObject, abilityActivationData);
            }
        }

        private static void activateAbility(CharacterObject characterObject, AbilityActivationData abilityActivationData)
        {
            List<Ability> abilties = characterObject.GetAbilities();
            if (abilties.Count > abilityActivationData.abilityIndex && abilties[abilityActivationData.abilityIndex].isEnabled()) 
            {
                bool success = abilties[abilityActivationData.abilityIndex].run(characterObject, abilityActivationData.attackPoint, abilityActivationData.direction);
                if(success)
                {
                    PushEvent(new PlayerActivateAbilityEvent(characterObject.playerId, abilityActivationData.abilityIndex));
                } 
                else
                {
                    //TODO send back forced client update
                }
            } 
            else
            {
                //TODO send back forced client update
            }
        }

        private static void SendGameState(int turnNumber)
        {
            //Compile a gamestate package which is sent to all players
            //Send game state to all, embedd the turn number
            //Construct game state
            List<CharacterStateData> characterStates = new List<CharacterStateData>();
            foreach (CharacterObject character in characters.Values)
            {
                characterStates.Add(new CharacterStateData(character.id, character.playerId, character.characterType, character.teamId, character.position, character.direction, character.hp, character.speed));
            }

            List<BuildingStateData> buildingStates = new List<BuildingStateData>();
            foreach (BuildingObject building in buildings.Values)
            {
                buildingStates.Add(new BuildingStateData(building.id, building.buildingType, building.teamId, building.position, building.hp));
            }

            GameState gameState = new GameState(turnNumber, characterStates, buildingStates, events);

            ServerSend.GameState(gameState);

            foreach (Event evnt in events)
            {
                Console.WriteLine("Sending event: " + evnt.ToString());
            }
            events.Clear();
        }

        public static void PlayerJoined(int _playerId, string _playerName)
        {
            Vector2 _newPlayerPosition;
            ETeam _teamId;
            CharacterObject obj;

            //Add player to a team
            if (!(TeamManager.marineTeam.GetNrOfPlayers() > TeamManager.centauriTeam.GetNrOfPlayers()))
            {
                _newPlayerPosition = Map.mapPosition + new Vector2(map.getAlienSpawnPoint().X, map.getAlienSpawnPoint().Y);
                TeamManager.centauriTeam.AddPlayer(_playerId);
                _teamId = TeamManager.centauriTeam.GetTeamId();
                obj = new RoachObject(_playerId, _newPlayerPosition);
            }
            else
            {
                _newPlayerPosition = Map.mapPosition + new Vector2(map.getMarineSpawnPoint().X, map.getMarineSpawnPoint().Y);
                TeamManager.marineTeam.AddPlayer(_playerId);
                _teamId = TeamManager.marineTeam.GetTeamId();
                obj = new MarineObject(_playerId, _newPlayerPosition);
            }

            Player newPlayer = new Player(_playerId, _playerName, _teamId);
            newPlayer.gameObj = obj;
            players.Add(_playerId, newPlayer);

            characters.Add(obj.id, obj);

            //Send init data to joined player (map, all players etc)
            ServerSend.Initialize(_playerId, map.GetMapId(), new List<Player>(players.Values));

            //Send player joined event to all players
            PushEvent(new PlayerJoinedEvent(_playerId, _playerName, _teamId));
        }

        public static void onPlayerCommand(PlayerCommandData cmd)
        {
            playerCommands.Enqueue(cmd);
        }

        public static void onPlayerLeft(int playerLeftId)
        {
            if (players.ContainsKey(playerLeftId))
            {
                Player player = players[playerLeftId];
                if (player.gameObj != null && characters.ContainsKey(player.gameObj.id)) {
                    characters.Remove(player.gameObj.id);
                }
                players.Remove(player.id);
                TeamManager.getTeam(player.teamId).RemovePlayer(playerLeftId);
                PushEvent(new PlayerDisconnectedEvent(player.id));
            }
        }

        public static void PushEvent(Event evnt) 
        {
            events.Enqueue(evnt);
        }

        public static Player GetPlayer(int id)
        {
            return players[id];
        }
    }
}
