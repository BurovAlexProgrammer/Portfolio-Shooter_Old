using _Project.Scripts.Main.UI;
using _Project.Scripts.Main.UI.Window;
using Main.Service;
using TMPro;
using UnityEngine.Serialization;

namespace _Project.Scripts.Main.AppServices.SceneServices
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