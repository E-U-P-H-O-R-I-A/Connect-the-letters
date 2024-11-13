using System;
using UnityEngine;

namespace CodeBase.ConnectLetters
{
    public struct PositionWord : IEquatable<PositionWord>
    {
        public int Length;
        public Vector2Int Position;
        public Orientation Orientation;
 

        public PositionWord(Vector2Int position, Orientation orientation, int length)
        {
            Length = length;
            Position = position;
            Orientation = orientation;
        }

        public bool Equals(PositionWord other)
        {
            return Position.Equals(other.Position) && Orientation == other.Orientation;
        }

        public override bool Equals(object obj)
        {
            return obj is PositionWord other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, (int)Orientation);
        }
    }
}