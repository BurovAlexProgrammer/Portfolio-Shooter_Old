using Main.UI;
using Main.UI.Window;
using Main.Services;
using TMPro;
using UnityEngine.Serialization;

namespace Main.Services
{
    public class GameUiServiceInstaller : BaseServiceInstaller
    {
        [FormerlySerializedAs("_healthBarView")] public BarView HealthBarView;
        [FormerlySerializedAs("_windowGamePause")] public WindowGamePause WindowGamePause;
        [FormerlySerializedAs("_windowGameOver")] public WindowGameOver WindowGameOver;
        [FormerlySerializedAs("_killCountText")] public TextMeshProUGUI KillCountText;
        [FormerlySerializedAs("_scoreCountText")] public TextMeshProUGUI ScoreCountText;
    }
}