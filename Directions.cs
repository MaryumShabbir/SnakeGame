using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Directions
    {
        public readonly static Directions left = new(0, -1);
        public readonly static Directions right = new(0, 1);
        public readonly static Directions Up = new(1, 0);
        public readonly static Directions Down = new(-1, 0);
        public int Rowoffset { get; }
        public int Columnoffset { get; }

        public Directions(int rowoffset, int columnoffset)
        {
            Rowoffset = rowoffset;
            Columnoffset = columnoffset;
        }

        public Directions opposite()
        {
            return new Directions(-Rowoffset, -Columnoffset);
        }

        public override bool Equals(object? obj)
        {
            return obj is Directions directions &&
                   Rowoffset == directions.Rowoffset &&
                   Columnoffset == directions.Columnoffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rowoffset, Columnoffset);
        }

        public static bool operator ==(Directions? left, Directions? right)
        {
            return EqualityComparer<Directions>.Default.Equals(left, right);
        }

        public static bool operator !=(Directions? left, Directions? right)
        {
            return !(left == right);
        }
    }
}

