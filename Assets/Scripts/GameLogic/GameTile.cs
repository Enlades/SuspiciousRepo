namespace ZyngaDemo.GameLogic{
    public struct GameTile
    {
        /// <summary>
        /// Logical index of the tile, [0 - 103]
        /// </summary>
        public int TileIndex { get; private set; }
        /// <summary>
        /// The number on the face of the tile
        /// </summary>
        public int TileNumber { get; private set; }
        public int TileColor { get; private set; }

        ///<summary>
        /// Never thought i'd create a property like this
        ///</summary>
        public bool IsOkey{get; private set;}

        public GameTile(int p_tileIndex, bool p_isOkey)
        {
            IsOkey = p_isOkey;
            TileIndex = p_tileIndex;
            TileNumber = (p_tileIndex % 52) % 13 + 1;
            TileColor = (p_tileIndex % 52) / 13;
        }

        ///<summary>
        /// Used for debugging purposes
        ///</summary>
        public override string ToString(){
            return "<color=" + ColorIntToString(TileColor) + ">" + TileNumber + "</color>";
        }

        ///<summary>
        /// Used for debugging purposes
        ///</summary>

        public string ToStringDetailed(){
            return "<color=" + ColorIntToString(TileColor) + ">" + TileNumber + "</color> :: " + TileIndex;
        }
        
        public bool CompareNumber(GameTile p_gameTile){
            return p_gameTile.TileNumber == TileNumber;
        }

        public bool CompareColor(GameTile p_gameTile)
        {
            return p_gameTile.TileColor == TileColor;
        }

        ///<summary>
        /// This was supposed to be used during Okey tile comparsion, not implemented
        ///</summary>
        public bool CompareTile(GameTile p_gameTile)
        {
            return TileIndex % 52 == p_gameTile.TileIndex % 52;
        }

        ///<summary>
        /// Arrangement algorithms create every possible GameTileGroups as if every tile is available multiple times, 
        /// this method is used for finding any collisions between groups so that the final Group only has unique tiles
        ///</summary>
        public bool IsDuplicateOf(GameTile p_gameTile){
            return TileIndex == p_gameTile.TileIndex;
        }

        ///<summary>
        /// Used for finding tiles for SortByColor
        ///</summary>
        public bool IsNextOf(GameTile p_gameTile){
            return p_gameTile.TileNumber == (TileNumber + 1);
        }

        ///<summary>
        /// Used for debugging purposes
        ///</summary>
        private static string ColorIntToString(int p_tileColor){
            switch (p_tileColor)
            {
                case 0:
                    {
                        return "red";
                    }
                case 1:
                    {
                        return "blue";
                    }
                case 2:
                    {
                        return "black";
                    }
                case 3:
                    {
                        return "yellow";
                }
                default:{
                    return "";
                }
            }
        }
    }
}