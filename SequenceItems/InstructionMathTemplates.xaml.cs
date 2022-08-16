using NINA.Core.Utility;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;

namespace NINA.InstructionMath.SequenceItems {
    [Export(typeof(ResourceDictionary))]
    public partial class PluginItemTemplate : ResourceDictionary {
        public PluginItemTemplate() {
            InitializeComponent();
            Logger.Info($"color={ButtonForegroundColor}");
        }

        public Color ButtonForegroundColor { get; set; } = (Application.Current.TryFindResource("ForegroundButtonBrush") as SolidColorBrush)?.Color ?? Color.FromRgb(255, 255, 255);
    }
}