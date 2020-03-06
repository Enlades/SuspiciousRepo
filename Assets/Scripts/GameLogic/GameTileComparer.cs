using System.Collections.Generic;

namespace ZyngaDemo.GameLogic{

    ///<summary>
    /// GameTile has a static reference to an object of this type
    ///</summary>
    public class GameTileNumberComparer : IComparer<GameTile>
    {
        public int Compare(GameTile p_x, GameTile p_y)
        {
            return p_x.TileNumber - p_y.TileNumber;
        }
    }

    ///<summary>
    /// GameTile has a static reference to an object of this type
    ///</summary>
    public class GameTileColorComparer : IComparer<GameTile>
    {
        public int Compare(GameTile p_x, GameTile p_y)
        {
            return p_x.TileColor - p_y.TileColor;
        }
    }

    ///<summary>
    /// While writing these comments, i realised they're the same.
    /// Also while writing this exact summary, i realised that they use different fields.
    ///</summary>
}