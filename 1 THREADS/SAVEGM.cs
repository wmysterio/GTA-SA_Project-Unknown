using GTA;
using GTA.Plugins;

public partial class MAIN {

    static SaveGame SAVE_GAME;

    public class SAVEGM : Thread {

        public override void START( LabelJump label ) {
            SaveGame.OverrideAngelPainPosition( 3.1238, -2520.989, 36.1484, 4.6886, -2519.3459, 35.6484, 0.0 );
            SAVE_GAME = new SaveGame();
        }

    }

}