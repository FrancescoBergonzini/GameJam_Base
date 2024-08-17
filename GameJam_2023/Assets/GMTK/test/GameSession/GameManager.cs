using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJamCore;

namespace GMTK
{
    public class GameManager : GameManagerBase
    {
        public Edge top, bottom, left, right;

        public Edge[] edges;

        public Vector3 GetEdgeSpawnPosition()
        {
            var edge = edges[Random.Range(0, edges.Length)];

            var point = RandomPointInBounds(edge.GetCollider().bounds);

            switch (edge.type)
            {
                case Edge.TYPE.TOP:

                    point.y += 1;

                    break;

                case Edge.TYPE.BOTTOM:

                    point.y -= 1;

                    break;

                case Edge.TYPE.LEFT:

                    point.x += 1;

                    break;

                case Edge.TYPE.RIGHT:

                    point.x -= 1;

                    break;

            }

            return point;


            Vector3 RandomPointInBounds(Bounds bounds)
            {
                return new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    Random.Range(bounds.min.z, bounds.max.z)
                );
            }
        }
    }
}

