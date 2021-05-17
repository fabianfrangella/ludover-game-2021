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

        public bool HasFinishedPath()
        {
            var finished = path.Count <= 0 || path == null;
            return finished;
        }
        
        public void GeneratePath(Vector2 from, Transform to, float speed)
        {
            path = new List<Vector2>();
            var nextDir = GetNextDirection(from, to.position, speed);
            CurrentDirection = from;
            while ((Vector2) to.position != nextDir)
            {
                nextDir = GetNextDirection(CurrentDirection, to.position, speed);
                path.Add(nextDir);
            }
            Debug.Log("THE PATH IS: " + GetPathString());
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

        public string GetPathString()
        {
            var pathS = "";
            foreach (var dir in path)
            {
                pathS+=  "X: " + dir.x + " Y: " + dir.y;
            }

            return pathS;
        }
        
    }

    public class Seeker
    {
        private Transform target;
        private Transform transform;
        private Vector2 initialTargetPosition;
        private Vector2 lastDirection;
        private Path path;

        public Seeker(Transform target, Transform transform)
        {
            this.target = target;
            this.transform = transform;
            initialTargetPosition = target.position;
            path = new Path();
            Debug.Log("GENERATING FIRST PATH");
            path.GeneratePath(transform.position, target, 1f);
        }
        
        public Vector2 GetNextDirectionTowardsTarget()
        {
            if (TargetHasMoved())
            {
                Debug.Log("GENERATING NEW PATH");
                SetNewTarget(target);
            }
            if (path.HasFinishedPath())
            {
                return lastDirection;
            }
            lastDirection = path.GetNextDirection();
            Debug.Log("NEXT DIRECTION: " + "{X: " + lastDirection.x + " Y: " + lastDirection.y + " }");
            return lastDirection;
        }

        private void SetNewTarget(Transform newTarget)
        {
            target = newTarget;
            initialTargetPosition = newTarget.position;
            path.GeneratePath(transform.position, newTarget, 1f);
        }
        
        private bool TargetHasMoved()
        {
            return Vector2.Distance(target.position, initialTargetPosition) > 0.1f;
        }
    }
}