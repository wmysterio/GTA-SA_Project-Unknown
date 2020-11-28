using GTA;
using GTA.Plugins;

public partial class MAIN {

    static Properties PROPERTIES;

    public class BUY_PRO : Thread {

        public override void START( LabelJump label ) {
            Properties.EnableAngarAssetsMoneyPickup = false;
            PROPERTIES = new Properties( SAVE_GAME );
        }

    }

}