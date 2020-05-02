namespace View
{
    using GXPEngine;

    // This contains useful functions
    public static class Helper
    {
        //------------------------------------------------------------------------------------------------------------------------
        //                                                  AlignToCenter()
        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Aligns a Sprite (and thus Canvas) to the center of the screen
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="alignHorizontally"></param>
        /// <param name="alignVertically"></param>
        public static void AlignToCenter(Sprite sprite, bool alignHorizontally, bool alignVertically)
        {
            if (alignHorizontally)
            {
                sprite.x = (Game.main.width - sprite.width) / 2;
            }
            if (alignVertically)
            {
                sprite.y = (Game.main.height - sprite.height) / 2;
            }
        }
    }
}
