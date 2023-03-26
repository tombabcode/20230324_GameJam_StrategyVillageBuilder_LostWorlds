using System.Collections.Generic;
using System.Linq;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Units;

/// <summary>
/// A* Path finding algorithm
/// Based on: https://www.youtube.com/watch?v=-L-WgKMFuhE
/// </summary>
public static class UnitPathFinder {

    private static List<PathNode> _nodesToCheck = new List<PathNode>( );
    private static List<PathNode> _nodesChecked = new List<PathNode>( );
    private static Tile _pathSourceTile = null;
    private static Tile _pathTargetTile = null;

    public static List<Tile> GetPath(MapManager map, Tile pathSourceTile, Tile pathTargetTile) {
        List<Tile> result = new List<Tile>();

        // Check if selected tiles are not empty
        if (pathSourceTile == null || pathTargetTile == null || pathSourceTile == pathTargetTile)
            return new List<Tile>( );

        // Preapre list of nodes
        _nodesToCheck.Clear( );
        _nodesChecked.Clear( );
        _pathSourceTile = pathSourceTile;
        _pathTargetTile = pathTargetTile;

        // Add start node to the check list
        _nodesToCheck.Add(new PathNode(_pathSourceTile));

        while (true) {
            // Get node with the lowest FCost
            PathNode currentNode = null;
            foreach (PathNode node in _nodesToCheck)
                if (currentNode == null || currentNode.FCost > node.FCost)
                    currentNode = node;

            // Remove that node from nodes to check, cause we are checking it right now
            _nodesToCheck.Remove(currentNode);

            // Mark current node as checked
            _nodesChecked.Add(currentNode);

            // Check if current node is the target
            if (currentNode.Data == pathTargetTile) {
                while (true) {
                    result.Add(currentNode.Data);
                    currentNode = currentNode.PreviousNode;
                    if (currentNode == null)
                        break;
                }

                result.Reverse( );
                break;
            }

            List<PathNode> neighbors = GetNeighbors(map, currentNode.Data);
            foreach (PathNode node in neighbors) {
                // Check if neighbor is closed
                if (_nodesChecked.Contains(node))
                    continue;

                float newMovementCost = currentNode.GCost + currentNode.GetDistanceBetweenTiles(node.Data);
                if (newMovementCost < node.GCost || !_nodesToCheck.Contains(node)) {
                    node.GCost = newMovementCost;
                    node.HCost = node.GetDistanceBetweenTiles(_pathTargetTile);
                    node.PreviousNode = currentNode;

                    if (!_nodesToCheck.Contains(node))
                        _nodesToCheck.Add(node);
                }
            }
        }

        return result;
    }

    private static List<PathNode> GetNeighbors(MapManager map, Tile tile) {
        List<PathNode> result = new List<PathNode>( );

        for (int y = -1; y <= 1; y++)
            for (int x = -1; x <= 1; x++) {
                // Skip if it's the tile that we are searching neighbors for
                if (x == 0 && y == 0)
                    continue;

                // Check if neighbor node is in "to check" list
                PathNode node = _nodesToCheck.Find(n => n.Data.X == tile.X + x && n.Data.Y == tile.Y + y);
                if (node != null) {
                    result.Add(node);
                    continue;
                }

                // Check if neighbor node is in "checked" list
                node = _nodesChecked.Find(n => n.Data.X == tile.X + x && n.Data.Y == tile.Y + y);
                if (node != null) {
                    result.Add(node);
                    continue;
                }

                // There is no such node. Add it
                Tile nodeTile = map.GetTile(tile.X + x, tile.Y + y);

                // This tile probably doesn't exist - is outside of the map
                if (nodeTile == null)
                    continue;

                node = new PathNode(nodeTile);
                result.Add(node);
            }

        return result;
    }

}
