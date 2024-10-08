using Microsoft.Xna.Framework;

class Peer
{
    public readonly PeerServer peerServer = new();
    public readonly PeerCient peerClient = new();

    public void Update(GameTime gameTime)
    {
        peerServer.Update(gameTime);
        peerClient.Update(gameTime);
    }

    public void Stop()
    {
        peerClient.client.DisconnectAll();
        peerClient.client.Stop();
        peerServer.server.DisconnectAll();
        peerServer.server.Stop();
    }
}