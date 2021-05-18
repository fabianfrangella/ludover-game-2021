using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Enemy
{
    public class PathTree
    {
        public PathTree nextPath;
        public Vector2 direction;

        public void GeneratePath(Vector2 from, Transform target, float speed)
        {
            direction = GetNextDirection(from, target.position, speed);
            if (direction == (Vector2) target.position) return;
            
            nextPath = new PathTree();
            nextPath.GeneratePath(direction, target, speed);
        }
        
        public bool IsLastNode()
        {
            return nextPath == null;
        }
        
        private Vector2 GetNextDirection(Vector2 from, Vector2 to, float speed)
        {
            var nextDirection = Vector2.MoveTowards(from, to, speed);
            if (!IsPositionOccupied(nextDirection))
            {
                Debug.DrawLine(from, nextDirection, Color.green, 1);
                return nextDirection;
            };
            
            var newDir = nextDirection + new Vector2(nextDirection.x, nextDirection.y + 0.1f);
            Debug.DrawLine(from, newDir, Color.green, 1);
            return nextDirection;
        }

        private static bool IsPositionOccupied(Vector2 position)
        {
            var col = Physics2D.OverlapCircle(position, 0.05f);
            var hasCollide = col != null && !col.CompareTag(TagEnum.Enemy.ToString()) && !col.CompareTag(TagEnum.Player.ToString());
            return hasCollide;
        }
        
        private static Vector2 Vector2FromAngle(float a)
        {
            a *= Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
        }
    }

    public class Seeker
    {
        private readonly Transform target;
        private readonly Transform transform;
        private Vector2 initialTargetPosition;
        private PathTree pathTree;

        public Seeker(Transform target, Transform transform)
        {
            this.target = target;
            this.transform = transform;
            initialTargetPosition = target.position;
            pathTree = new PathTree();
            Debug.Log("GENERATING FIRST PATH");
            pathTree.GeneratePath(transform.position,target,1f);
        }
        
        public Vector2 GetNextDirectionTowardsTarget()
        {
            if (TargetHasMoved())
            {
                Debug.Log("GENERATING NEW PATH");
                initialTargetPosition = target.position;
                pathTree.GeneratePath(transform.position,target,1f);
            }
            if (pathTree.IsLastNode()) 
                return pathTree.direction;
            
            pathTree = pathTree.nextPath;
            return pathTree.direction;
        }
        private bool TargetHasMoved()
        {
            return Vector2.Distance(target.position, initialTargetPosition) > 0.1f;
        }
    }
}