using GTA;
using GTA.Plugins;

public partial class MAIN {

    const double CUTSCENE_VOLUME = 0.2;

    // ---------------------------------------------------------------------------------------------------------------------------

    static AudioBackground AUDIO_BG;

    // ---------------------------------------------------------------------------------------------------------------------------

    public class AUDIOBG : Thread { public override void START( LabelJump label ) { AUDIO_BG = new AudioBackground(); } }

}