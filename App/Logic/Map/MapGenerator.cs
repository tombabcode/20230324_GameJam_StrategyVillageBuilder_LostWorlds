using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Controllers;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Buildings;
using _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map.Environment;
using System;
using System.Collections.Generic;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Map;

public static class MapGenerator {

    public static Tile[] Generate(ContentController content, ConfigController config, InputController input, int size, out Tile villageTile) {
        Tile[] result = new Tile[size * size];
        float[] distances = new float[size * size];

        float radius = (size * ConfigController.TILE_SIZE) / 2f;

        // Create empty tiles in circle area
        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                int id = y * size + x;

                // Calculate positions
                float posX = (x - size / 2f) * ConfigController.TILE_SIZE + ConfigController.TILE_SIZE * 0.5f;
                float posY = (y - size / 2f) * ConfigController.TILE_SIZE + ConfigController.TILE_SIZE * 0.5f;

                // Set distance
                distances[id] = (float)Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2));

                // Calculate distance from the center. We want the map as a circle
                if (distances[id] >= radius)
                    continue;

                // Create tile
                result[id] = new Tile(content, input, x, y);
            }
        }

        // Helper variables
        List<int> available = new List<int>( );

        // --- Village
        // Calculate on which tiles can village be placed
        for (int i = 0; i < distances.Length; i++)
            if (distances[i] < radius * .2f && result[i] != null)
                available.Add(i);

        // If there is no available tiles - place it at the center
        int resultID = available.Count == 0 ? (size * size) / 2 : available[config.Random.Next(0, available.Count)];
        result[resultID].SetTileObject(new Village(content, input, result[resultID].X, result[resultID].Y));
        villageTile = result[resultID];

        // --- Monoliths
        // Each direction will have one monolith (top, bottom, left, right)
        int monolithMargin = 30; // If is set to 45 - there is not distance between monoliths (top can be next to right, etc., separation is 0 degrees). 30 creates ((45 - 30) * 2) = 30 degree angle separation between directions
        for (int i = 0; i < 4; i++) {
            available.Clear( );

            // Calculate on which tiles can monolith be placed for current direction
            for (int j = 0; j < distances.Length; j++)
                if (distances[j] > radius * .75f && result[j] != null) {
                    // Calculate angle for current direction
                    int y = j / size;
                    int x = j % size;
                    float angle = (float)((Math.Atan2(y - size / 2, x - size / 2) * (180f / Math.PI)) + 360) % 360;

                    // Angle matches direction
                    if (angle + 360 > i * 90 - monolithMargin + 360 && angle + 360 < i * 90 + monolithMargin + 360)
                        available.Add(j);
                }

            // Place monolith
            if (available.Count > 0) {
                resultID = available[config.Random.Next(0, available.Count)];
                result[resultID].SetTileObject(new Monolith(content, input, result[resultID].X, result[resultID].Y));
            }
        }

        // --- Meadow
        float meadowProbability = .1f;
        for (int i = 0; i < result.Length; i++) {
            Tile tile = result[i];
            if (tile != null && !tile.HasTileObject( ) && config.Random.NextDouble( ) < meadowProbability)
                tile.SetTileObject(new Meadow(content, input, tile.X, tile.Y));
        }

        // --- Forest
        float forestProbability = .7f;
        for (int i = 0; i < result.Length; i++) {
            Tile tile = result[i];
            if (tile != null && !tile.HasTileObject( ) && config.Random.NextDouble( ) < forestProbability)
                tile.SetTileObject(new Forest(content, input, tile.X, tile.Y));
        }

        return result;
    }

}
