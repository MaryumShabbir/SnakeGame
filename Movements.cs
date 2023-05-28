using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Movements
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Directions Dir { get; private set; }
        public int Score { get; private set; }
        public bool Game_Over { get; private set; }

        private readonly LinkedList<Directions> dirChanges = new LinkedList<Directions>();

        private readonly LinkedList<Position> SnakePositions = new LinkedList<Position>();
        private readonly Random random = new Random();
        public Movements(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Directions.right;

            Addsnake();
            AddFood();

        }
        private void Addsnake()
        {
            int r = Rows / 2;
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.snake;
                SnakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> emptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(emptyPositions());
            if (empty.Count == 0)
            {
                return;
            }
            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.food;
        }
        public Position HeadPosition()
        {
            return SnakePositions.First.Value;

        }
        public Position TailPosition()
        {
            return SnakePositions.Last.Value;
        }

        public IEnumerable<Position> snakePositions()
        {
            return SnakePositions;
        }

        private void AddHead(Position pos)
        {
            SnakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.snake;
        }

        private void RemoveTail()
        {
            Position tail = SnakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.empty;
            SnakePositions.RemoveLast();
        }

        private Directions GetlastDirections()
        {
            if(dirChanges.Count==0) {
                return Dir;
            }
            return dirChanges.Last.Value;
        }

        private bool CanChangeDirection(Directions newDir)
        {
            if (dirChanges.Count==2)
            {
                return false;
            }
            Directions LastDir= GetlastDirections();
            return newDir !=LastDir && newDir != LastDir.opposite();
        }

        public void ChangeDirections(Directions dir)
        {
            if (!CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
            dirChanges.AddLast(dir);
        }
        private bool boundaryGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;

        }

        private GridValue WillHit(Position newHeadPos)
        {
            if (boundaryGrid(newHeadPos))
            {
                return GridValue.boundary;
            }

            if (newHeadPos == TailPosition())
            {
                return GridValue.empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        public void Move()
        {

            if (dirChanges.Count > 0)
            {
                Dir = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.boundary || hit == GridValue.snake)
            {
                Game_Over = true;
            }
            else if (hit == GridValue.empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.food)
            {
                AddHead(newHeadPos);
                Score++;
                AddFood();
            }
        }


    }

}
    

