using System.IO.Ports;

namespace Serial_Detector
{
    internal class Program
    {
        static SerialPort serialPort;
        static void Main(string[] args)
        {
            string[] ports = SerialPort.GetPortNames();

            Console.WriteLine("The following serial ports were found:");

            // Display each port name to the console.
            foreach (string port in ports)
            {
                try
                {
                    Console.WriteLine(port);

                    serialPort = new SerialPort(port, 9600, Parity.Odd);
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                    serialPort.Open();
                    serialPort.ReadTimeout = 1000;
                    serialPort.WriteTimeout = 1000;

                    if (serialPort.IsOpen)
                    {
                        serialPort.WriteLine("AT");
                        Thread.Sleep(1000);
                        serialPort.WriteLine("AT+GMM");
                        Thread.Sleep(1000);

                        serialPort.Close();
                    }

                } catch (Exception e) { Console.WriteLine(e.ToString()); }
            }

            Console.ReadLine();
        }
        private static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            Console.WriteLine(serialPort.ReadLine());
        }
    }
}
