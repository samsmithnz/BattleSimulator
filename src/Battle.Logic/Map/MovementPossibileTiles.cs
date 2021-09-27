﻿using System.Collections.Generic;
using System.Numerics;

namespace Battle.Logic.Map
{
    public static class MovementPossibileTiles
    {
        public static List<Vector3> GetMovementPossibileTiles(string[,,] map, Vector3 sourceLocation, int range)
        {
            List<Vector3> possibleTiles = MapCore.GetMapArea(map, sourceLocation, range, false);
            List<Vector3> verifiedTiles = new List<Vector3>();
            foreach (Vector3 item in possibleTiles)
            {
                PathFindingResult result = PathFinding.FindPath(sourceLocation, item, map);
                if (result.Tiles.Count > 0 && result.Tiles[result.Tiles.Count - 1].TraversalCost <= range)
                {
                    verifiedTiles.Add(item);
                }
            }
            return verifiedTiles;
        }

        //public static List<KeyValuePair<Vector3, int>> GetMovementPossibileTiles2(string[,,] map, Vector3 sourceLocation, int range, int actionPoints)
        //{
        //    List<Vector3> possibleTiles = MapCore.GetMapArea(map, sourceLocation, range, false);
        //    List<Vector3> verifiedTiles = new List<Vector3>();
        //    foreach (Vector3 item in possibleTiles)
        //    {
        //        PathFindingResult result = PathFinding.FindPath(sourceLocation, item, map);
        //        if (result.Tiles.Count > 0 && result.Tiles[result.Tiles.Count - 1].TraversalCost <= range)
        //        {
        //            verifiedTiles.Add(item);
        //        }
        //    }

        //    List<KeyValuePair<Vector3, int>> results = new List<KeyValuePair<Vector3, int>>();

        //    return results;
        //}
    }
}
