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
        peerClient.Stop();
        peerServer.Stop();
    }
}