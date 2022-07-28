using System.Collections.Concurrent;

namespace HILoGameWebApi.PersistenceData.InMemory
{


    public class ConnectionMapping<T>
    {
        public record ConnectionPlayer(string Name, T Group, string ConnectionId);

        private readonly ConcurrentDictionary<T, HashSet<ConnectionPlayer>> _connections = new ConcurrentDictionary<T, HashSet<ConnectionPlayer>>();

        public int Count
        {
            get => _connections.Count;
        }

        public void Add(T key, string name, string connectionId)
        {
            lock (_connections)
            {
                HashSet<ConnectionPlayer> connectionPlayer;

                if (!_connections.TryGetValue(key, out connectionPlayer))
                {
                    connectionPlayer = new HashSet<ConnectionPlayer>();
                    _connections.TryAdd(key, connectionPlayer);
                }

                lock (connectionPlayer)
                {
                    connectionPlayer.Add(new(name, key, connectionId));
                }
            }
        }

        public IEnumerable<ConnectionPlayer> GetConnections(T key)
        {
            HashSet<ConnectionPlayer> connectionPlayer;

            if (_connections.TryGetValue(key, out connectionPlayer))
            {
                return connectionPlayer;
            }

            return Enumerable.Empty<ConnectionPlayer>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<ConnectionPlayer> connectionPlayer;

                if (!_connections.TryGetValue(key, out connectionPlayer))
                {
                    return;
                }

                lock (connectionPlayer)
                {
                    var connectionToRemote = _connections
                                                            .Where(conn => conn.Value.Equals(connectionId))
                                                            .Select(select => select.Value.FirstOrDefault())
                                                            .FirstOrDefault()!;

                    connectionPlayer.Remove(connectionToRemote);

                    if (connectionPlayer.Count == 0)
                    {
                        _connections.TryRemove(key, out _);
                    }
                }
            }
        }

        public ConnectionPlayer Remove(string connectionId)
        {
            ConnectionPlayer connectionRemove = default!;

            lock (_connections)
            {
                T group = _connections
                                .Where(conn => conn.Value.Count(conn => conn.ConnectionId.Equals(connectionId)) == 1)
                                .Select(select => select.Key).FirstOrDefault()!;

                if (group is not null)
                {
                    HashSet<ConnectionPlayer> connections = default!;

                    if (_connections.TryGetValue(group, out connections!))
                    {
                        if (connections.Count > 1)
                        {
                            Predicate<ConnectionPlayer> queryPlayer = (remove) => remove.ConnectionId == connectionId;

                            connectionRemove = connections.FirstOrDefault(new Func<ConnectionPlayer, bool>(queryPlayer))!;

                            connections.RemoveWhere(queryPlayer);
                        }
                        else
                            _connections.TryRemove(group, out _);
                    }
                }

            }

            return connectionRemove;
        }

    }
}
