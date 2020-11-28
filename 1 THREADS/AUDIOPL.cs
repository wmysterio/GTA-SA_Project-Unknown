using GTA;
using GTA.Plugins;

public partial class MAIN {

    static AudioPlayer AUDIO_PL;

    // ---------------------------------------------------------------------------------------------------------------------------

    public class AUDIOPL : Thread { public override void START( LabelJump label ) { AUDIO_PL = new AudioPlayer(); } }

}