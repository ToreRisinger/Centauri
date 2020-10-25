public class PacketTypes
{
    /// <summary>
    /// Sent from server to client.
    /// </summary>
    public enum ServerPackets
    {
        welcome = 1,
        initialize,
        spawnPlayer,
        gameState
    }

    /// <summary>
    /// Sent from client to server.
    /// </summary>
    public enum ClientPackets
    {
        welcomeReceived = 1,
        playerCommand
    }
}