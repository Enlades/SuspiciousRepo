using System.Collections.Generic;

namespace ZyngaDemo.GameLogic{

    public class GameTileNumberComparer : IComparer<GameTile>
    {
        public int Compare(GameTile p_x, GameTile p_y)
        {
            return p_x.TileNumber - p_y.TileNumber;
        }
    }

    public class GameTileColorComparer : IComparer<GameTile>
    {
        public int Compare(GameTile p_x, GameTile p_y)
        {
            return p_x.TileColor - p_y.TileColor;
        }
    }
}