using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Enemy
{
    public class Path
    {
        public List<Vector2> path;
        private Vector2 CurrentDirection { get; set; }
        public Path()
        {
            path = new List<Vector2>();
        }

        public Vector2 GetNextDirection()
        {
            if (path.Count <= 0) return Vector2.zero;
            var next = path[0];
            var tail = path.Skip(1);
            path = new List<Vector2>(tail);
            return next;
        }

        public Vector2 GetLastDirection()
        {
            var len = path.Count - 1;
            if (len < 0) return Vector2.zero;
            return path[len];
        }

        public bool HasFinishedPath()
        {
            var finished = path.Count <= 0 || path == null;
            return finished;
        }
        
        public void GeneratePath(Vector2 from, Vector2 to, float speed)
        {
            path = new List<Vector2>();
            var nextDir = GetNextDirection(from, to, speed);
            CurrentDirection = from;
            while (to != nextDir)
            {
                nextDir = GetNextDirection(CurrentDirection, to, speed);
                path.Add(nextDir);
            }
        }

        private Vector2 GetNextDirection(Vector2 from, Vector2 to, float speed)
        {
            var nextDirection = Vector2.MoveTowards(from, to, speed);
            if (PositionIsOcuppied(nextDirection))
            {
                nextDirection = GetNextDirection((Vector2) from, to - new Vector2(-1, -1), speed);
            }
            CurrentDirection = nextDirection;
            return nextDirection;
        }

        private bool PositionIsOcuppied(Vector2 position)
        {
            return false;
        }
        
    }

    public class Seeker
    {
        private Transform target;
        private Transform transform;
        private Path path;

        public Seeker(Transform target, Transform transform)
        {
            this.target = target;
            this.transform = transform;
            path = new Path();
            path.GeneratePath(transform.position, target.position, 1f);
        }

        public bool HasFinishPath()
        {
            return path.HasFinishedPath();
        }
        public Vector2 GetNextDirectionTowardsTarget()
        {
            if (TargetHasMoved())
            {
                path.GeneratePath(transform.position, target.position, 1f);
            }
            return path.GetNextDirection();
        }
        
        bool TargetHasMoved()
        {
            return Vector2.Distance(target.position, path.GetLastDirection()) > 0.5f;
        }
    }
}