using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSever
{
    public class Chasing
    {
        public bool IsGameOver { get; private set; }

        public bool IsCorrect { get; private set; }

        public Client Player1 { get; set; }

        public Client Player2 { get; set; }

        private readonly int[] field = new int[5];
        private int movesLeft = 5;

        public Chasing()
        {
            //Reset game
            for(var i=0;i<field.Length;i++)
            {
                field[i] = -1;
            }
        }
        /// <summary>
        /// Insert a correct at a given index for a given player
        /// </summary>
        /// <param name="player">The player number should be 0 or 1</param>
        /// <param name="position">The index where to place the marker, should be between 0 and 5</param>
        /// <returns>True if a winner was found</returns>
        public bool Play(int player ,int posion)
        {
            if (IsGameOver)
                return false;
            PlaceMarker(player,posion);
            return true;
        }
        /// <summary>
        /// Places a marker at the given position for the given player as long as the position is marked as -1
        /// </summary>
        /// <param name="player">The player number should be 0 or 1</param>
        /// <param name="position">The position where to place the marker, should be between 0 and 5</param>
        /// <returns>True if the marker position was not already taken</returns>
        private bool PlaceMarker(int player, int position)
        {
            movesLeft -= 1;

            if (movesLeft <= 0)
            {
                IsGameOver = true;
                IsCorrect = true;
                return false;
            }

            if (position > field.Length)
                return false;
            if (field[position] != -1)
                return false;

            field[position] = player;

            return true;
        }
        
    }
}