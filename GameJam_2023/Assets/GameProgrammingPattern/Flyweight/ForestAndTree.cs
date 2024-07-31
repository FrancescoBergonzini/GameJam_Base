using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ForestAndTree : MonoBehaviour
{
    #region not optimized

    public class Tree
    {
        Mesh _mesh;
        Texture _bark;
        Texture _leaves;
        Vector3 _pos;
        double _height;
        double _thikness;
        Color _backTint;
        Color _leafTint;
    }
    #endregion

    #region optimized

    class TreeModel
    {
        Mesh _mesh;
        Texture _bark;
        Texture _leaves;
    }

    class OptmizedTree
    {
        TreeModel _model;

        Vector3 _pos;
        double _height;
        double _thikness;
        Color _backTint;
        Color _leafTint;
    }
    #endregion

    //A Place to Put Down Roots
    #region not optimized
    public enum Terrain
    {
        GRASS,
        HILL,
        RIVER
    }

    public class World
    {
        //array multidimensionale...
        private Terrain[,] tiles;

        int GetMovementCost(int x, int y)
        {
            switch(tiles[x, y])
            {
                case Terrain.GRASS: return 1; 
                case Terrain.HILL: return 2;
                case Terrain.RIVER: return 3;

            }

            return -1;
        }

        bool IsWater(int x, int y)
        {
            switch (tiles[x, y])
            {
                case Terrain.GRASS: return false;
                case Terrain.HILL: return false;
                case Terrain.RIVER: return true;

            }

            return false;
        }
    }
    #endregion

    #region optimized

    class OptimizedTerrain
    {
        //si possono mettere anche come const, l'impostante è che non siano
        //modificabili dall'esterno; sono oggetti Flyweight;
        int _moveCost;
        bool _isWater;
        Texture _texture;

        public int MoveCost => _moveCost;
        public bool IsWater => _isWater;
        public Texture Texture => _texture;

        public OptimizedTerrain(int _moveCost, bool _isWater, Texture _texture)
        {
            this._moveCost = _moveCost;
            this._isWater = _isWater;
            this._texture = _texture;
        }


    }

    class OptimizedWorld 
    {
        //
        Texture fake_texture;

        //
        OptimizedTerrain _grassTerrain;
        OptimizedTerrain _hillTerrain;
        OptimizedTerrain _riverTerrain;

        public OptimizedWorld()
        {
            _grassTerrain = new OptimizedTerrain(1, false, fake_texture);
            _hillTerrain = new OptimizedTerrain(3, false, fake_texture);
            _riverTerrain = new OptimizedTerrain(2, true, fake_texture);
        }

        OptimizedTerrain[,] _tiles;
        int WIDTH;
        int HEIGHT;

        void GenerateTerrain()
        {
            //fill
            for(int x = 0; x < WIDTH; x++)
            {
                for(int y = 0; y < HEIGHT; y++)
                {
                    //randomize...
                    if(UnityEngine.Random.Range(0, 10) == 0)
                    {
                        _tiles[x, y] = _hillTerrain;
                    }
                    else
                    {
                        _tiles[x, y] = _grassTerrain;
                    }
                }
            }

            //lay a river
            int _x = Random.Range(0, WIDTH);
            for(int y = 0; y < HEIGHT; y++)
            {
                _tiles[_x, y] = _riverTerrain;
            }
        }

        OptimizedTerrain getTerrain(int x, int y) => _tiles[x, y];

        int GetMovementCost(int x, int y)
        {
            return getTerrain(x,y).MoveCost;
        }

        bool IsWater(int x, int y)
        {
            return getTerrain(x, y).IsWater;
        }
    }

    #endregion
}
