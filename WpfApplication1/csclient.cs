using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class csclient {
    //change start client to take 
    /*color of robot
     * action we want it to take
     *  IP addresses
     * blue == 12345
     * green=12346
     * red==12347
     */
    public static void StartClient(string color, string command) {
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];
        int IPAddress;
        IPAddress = 0;
        // Connect to a remote device.
        try {
            // Establish the remote endpoint for the socket.
            // This example uses port 11000 on the local computer.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            if (color == "red")
            {
                IPAddress = 12347;
            }
            else if (color == "blue")
            {
                IPAddress = 12345;
            }
            else if (color == "green")
            {
                IPAddress = 12346;
            }
            IPEndPoint remoteEP = new IPEndPoint(ipAddress,IPAddress);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork, 
                SocketType.Stream, ProtocolType.Tcp );

            // Connect the socket to the remote endpoint. Catch any errors.
            try {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                byte[] msg = Encoding.ASCII.GetBytes(command);
                //l=left, r=right, f=forward, a=aboutface TODO stop

                // Send the data through the socket.
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(bytes,0,bytesRec));

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            } catch (ArgumentNullException ane) {
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());
            } catch (SocketException se) {
                Console.WriteLine("SocketException : {0}",se.ToString());
            } catch (Exception e) {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        } catch (Exception e) {
            Console.WriteLine( e.ToString());
        }
    }
}