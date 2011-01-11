using System;
using System.Threading;
using System.Collections;
using System.Net.Sockets;
using System.IO;

using Lidgren.Network;

namespace Sept1983Server
{
	class ProgramServer
	{

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private static ArrayList initSequenceList(String folder) {
            ArrayList sequences = new ArrayList();
            DirectoryInfo di = new DirectoryInfo("./Scripts/");
            FileInfo[] rgFiles = di.GetFiles("*.cs");
            foreach (FileInfo fi in rgFiles)
            {
                var name = fi.Name.Substring(0, fi.Name.LastIndexOf("."));
                sequences.Add(name);
            }
            return sequences;
        }
        

		public static void Run()
		{
            // initialize
            int mapDimension = 16;
            Map mapServer = Map.GenerateShipsOnMap(new Map(mapDimension));
            Map mapHuman = Map.GenerateShipsOnMap(new Map(mapDimension));

            Player playerServer = new Player("Server", mapServer); // non-player-character ie our server
            Player playerHuman = new Player("Human", mapHuman); // player character
            int currentRound = 0;

            ArrayList sequences = initSequenceList("Scripts");
        

		   // TODO initiate Server
            // server listens to port


            try 
            {
              // TODO start server


			// schedule initial sending of position updates
			double nextSendUpdates = NetTime.Now;

			// run until escape is pressed
			//while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
            while (true)

            {
				NetIncomingMessage msg;
				while ((msg = server.ReadMessage()) != null)
				{
					switch (msg.MessageType)
					{
						case NetIncomingMessageType.DiscoveryRequest:
							// Server received a discovery request from a client; send a discovery response (with no extra data attached)
							server.SendDiscoveryResponse(null, msg.SenderEndpoint);
							break;
						case NetIncomingMessageType.VerboseDebugMessage:
						case NetIncomingMessageType.DebugMessage:
						case NetIncomingMessageType.WarningMessage:
						case NetIncomingMessageType.ErrorMessage:
							// Just print diagnostic messages to console
							Console.WriteLine(msg.ReadString());
							break;
						case NetIncomingMessageType.StatusChanged:
							NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
							if (status == NetConnectionStatus.Connected)
							{
								// A new player just connected!
								Console.WriteLine(NetUtility.ToHexString(msg.SenderConnection.RemoteUniqueIdentifier) + " connected!");

                                // create new player object
                                // initialize new game

                                //playerServer.startGame();
                                //playerHuman.startGame();

							}

							break;
						case NetIncomingMessageType.Data:

                            String responseString = "Nothing happend";

                            // player round

							// The client sent input to the server
							
                            String sequenceName = msg.ReadString();

                            int round = msg.ReadInt32();
                            if (round > currentRound)
                            {
                                currentRound = round;

                                if (sequences.Contains(sequenceName))
                                {
                                
                                // choose and fire sequence

                                var executer = new SequenceExecuter(playerServer.map);
                                var resultMessagePlayer = executer.LoadScript(sequenceName);


                                // switch round to npc

                                // fire random sequence
                                int randomIndex = RandomNumber(0, sequences.Count);
                                sequenceName = (String)sequences[randomIndex];
                                executer = new SequenceExecuter(playerHuman.map);
                                var resultMessageNPC = executer.LoadScript(sequenceName);

                                // send response to client

                                
                                String battlefield = Battlefield.Draw(playerHuman, playerServer);
                                responseString = "Player: " + resultMessagePlayer + "\n" + battlefield + " \nComputer: " + resultMessageNPC;
                                } else {
                                    responseString = "FireSequence not recognized!";
                                }
                            } else {
                                responseString = "Game round invalid";
                            }

						// TODO server response implementation




							break;
					} // switch messageType

				} // while readMessage

				// sleep to allow other processes to run smoothly
				Thread.Sleep(1);
			} // while thread

            }
            catch (SocketException ex)
            {
                System.Console.WriteLine("Socket could not be initiated, is the port already used?!");
                System.Windows.Forms.MessageBox.Show("Socket could not be initiated, is the port already used?!", 
                    "September 1983 Server");
            }

		}
	}
}
