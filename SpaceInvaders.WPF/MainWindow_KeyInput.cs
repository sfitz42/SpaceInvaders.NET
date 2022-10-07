using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaceInvaders.WPF
{
    /// <summary>
    /// Key input handling for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public void SetKey(Key key, bool pressed)
        {
            if (key == Key.Escape)
                Close();

            var port0Mapping = InputMappings.InputPort0Mapping;
            var port1Mapping = InputMappings.InputPort1Mapping;
            var port2Mapping = InputMappings.InputPort2Mapping;

            if (port0Mapping.ContainsKey(key))
                _arcadeMachine.InputPort0.HandleInput(port0Mapping[key], pressed);

            if (port1Mapping.ContainsKey(key))
                _arcadeMachine.InputPort1.HandleInput(port1Mapping[key], pressed);

            if (port2Mapping.ContainsKey(key))
                _arcadeMachine.InputPort2.HandleInput(port2Mapping[key], pressed);
        }

        public void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            SetKey(keyEventArgs.Key, true);
        }

        public void OnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            SetKey(keyEventArgs.Key, false);
        }
    }
}
