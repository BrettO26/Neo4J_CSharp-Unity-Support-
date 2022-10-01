using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Text;
namespace Neo4JExample
{
    class Program
    {
        #region Login Details
        static string myUri = "";
        static string myUsername = "";
        static string myPassword = "";
        #endregion
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! [OUTDATED EXAMPLE!]");

            int CustomPort = 11567; // Default Port is 11000 so this isnt needed.
            int PacketSize = 256; // The size of the return packets.
            bool ShowDetails = false; // Debug the login details on the neo4j console.
            bool ShowConsole = true; // Show the console. (intended for use with unity)
            string Path = @"Example: C:\Neo4J_CSharp-Unity-Support-\Neo4J_CSharp\net5.0\Neo4J_CSharp.exe"; // The path of the Neo4J application (.exe) on your computer.
            //Create and bind the socket.
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, CustomPort));
            //Im guna be honest idk much about this line besides it listens for incoming connections.
            socket.Listen(10);
            //Start the Neo4j application.
            Process Neo4JProcess = Process.Start(Path, $"{CustomPort},{ShowDetails}");
            //Connect to the Neo4j application.
            socket = socket.Accept();
            //Uri|Username|Password|ShowConsole|PacketSize|
            string SetupInfo = $"{myUri}|{myUsername}|{myPassword}|{(ShowConsole ? 1 : 0)}|{PacketSize}|";
            //Send the setup info.
            socket.Send(UTF8(SetupInfo));
            //Responce
            socket.Receive(new byte[2]);
            //The query that were going to send.
            string Query = "Match(node:TestNode) Return Properties(node)";
            //The key that were going to use (EX: in this case we want the properties of node.
            //Normaly the key would be "Properties(node)" but in this case were saying "as data" allowing us to use data as the key.
            string Key = "Properties(node)";
            //If we want a single result. (Single result would return somthing like "64". but mutiple would look somthing like "Data:64,Data2:128,name:testNode" )
            bool Single = false;
            //Put all of the data that we wana send into one string and space the data with slashes.
            //@ states that the new string will not use \ commands.
            //$ allows you to put variables in you string without having to go "text" + variable.ToString() + "more text" or ("text {0} more text", variable).
            string QueryInfo = @$"{Query}\{Key}\{Single}\";
            //Actually send the data.
            socket.Send(UTF8(QueryInfo));
            //Create a new buffer for the data were about to recieve.
            byte[] ResponceData = new byte[PacketSize];
            //Recive the data and fill the buffer.
            socket.Receive(ResponceData);
            //Convert the result into text.
            string QueryResults = UTF8(ResponceData);
            //Check for an error.
            if (QueryResults == "ERROR")
                //throw a new error that the user will know about.
                throw new Exception("There was an error processing you query");
            //We dont want the whitespace behind the result data so we split by the semicolion at the end of the result.
            string TrimmedQueryResults = QueryResults.Split(';')[0];
            //Split into its individual properties.
            string[] SplitQueryResult = TrimmedQueryResults.Split(',');
            //Iterate over every single property.
            foreach (string Result in SplitQueryResult)
            {
                //Split the property into name and data.
                string[] SplitResult = Result.Split(':');
                //Log the data that we got.
                Console.WriteLine($"Property: {SplitResult[0]} Value: {SplitResult[1]}");
            }
            //Close the neo4J application.
            Neo4JProcess.Kill();
            //Read line to prevent the application from just closing.
            Console.WriteLine("Press ENTER to close the application");
            Console.ReadLine();
            //If you wanted to you can keep sending querys and it will keep sending you data back.
            //If you just disconnect from the socket it will try to reconnect for a little bit and then close the application.
            //The source code most likly will not be made avalible, atleast at the time of writing this.
            //
            //Neo4J application: .Net (5.0) 
        }

        //Quick way of converting data in the form of byte[]'s into text.
        public static string UTF8(byte[] data) => Encoding.UTF8.GetString(data);
        //Quick way of converting text into byte[]'s.
        public static byte[] UTF8(string text) => Encoding.UTF8.GetBytes(text);
    }
}
