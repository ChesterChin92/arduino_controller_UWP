using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Maker.Firmata;
using Microsoft.Maker.Serial;
using Microsoft.Maker.RemoteWiring;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ArduinoControllerMain
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IStream connection;
        RemoteDevice arduino;

        public MainPage()
        {
            this.InitializeComponent();

            connection = new UsbSerial("VID_2341","PID_0043");
            arduino = new RemoteDevice(connection);

            connection.ConnectionEstablished += OnConnectionEstablished;

            connection.begin(57600, SerialConfig.SERIAL_8N1);
        }

        private void OnConnectionEstablished() {
            var action = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new Windows.UI.Core.DispatchedHandler(() =>
            {
                onBtn.IsEnabled = true;
                offBtn.IsEnabled = true;
            }));
        }

        private void onBtn_Click(object sender, RoutedEventArgs e)
        {
            //Turn LED conmected to pin 13 is ON
            arduino.digitalWrite(13, PinState.HIGH);
        }


        private void offBtn_Click(object sender, RoutedEventArgs e)
        {

            //Turn LED conmected to pin 13 is OFF
            arduino.digitalWrite(13, PinState.LOW);
        }
    }
}
