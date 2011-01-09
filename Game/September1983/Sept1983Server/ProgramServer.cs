using System;
using System.Threading;
using System.Collections;

using Lidgren.Network;

namespace Sept1983Server
{
	class ProgramServer
	{

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

		public static void Run()
		{

            // initialize

            Player playerOne = new Player("Player"); // non-player-character ie our server
            Player playerTwo; // player character

            ArrayList sequences = new ArrayList();
            sequences.Add("FireSequenceAlpha.cs");

			NetPeerConfiguration config = new NetPeerConfiguration("xnaapp");
			config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
			config.Port = 14242;

			// create and start server
			NetServer server = new NetServer(config);
			server.Start();

			// schedule initial sending of position updates
			double nextSendUpdates = NetTime.Now;

			// run until escape is pressed
			//while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
			while(true)
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
                                playerTwo = new Player("Computer");

                                playerOne.startGame();
                                playerTwo.startGame();

							}

							break;
						case NetIncomingMessageType.Data:
							
                            // player round

							// The client sent input to the server
							
                            String sequenceName = msg.ReadString();

                            // choose and fire sequence

                            var executer = new SequenceExecuter(playerOne.map);
                            var resultMessagePlayer = executer.LoadScript(sequenceName);


                            // switch round to npc

                            // fire random sequence
                            int randomIndex = RandomNumber(0, sequences.Count);
                            sequenceName = (String)sequences[randomIndex];
                            executer = new SequenceExecuter(playerTwo.map);
                            var resultMessageNPC = executer.LoadScript(sequenceName);

                            // send response to client

                            NetOutgoingMessage om = server.CreateMessage();
                            var responseString = resultMessagePlayer + "\n\n Map \n\n" + resultMessageNPC;
                            om.Write(responseString);
                            // send to every player, which is only one in our case
                            foreach (NetConnection player in server.Connections) 
                            {
                                server.SendMessage(om, player, NetDeliveryMethod.Unreliable);
                            }

							break;
					}

					//
					// send position updates 30 times per second
					//
					/*double now = NetTime.Now;
					if (now > nextSendUpdates)
					{
						// Yes, it's time to send position updates

						// for each player...
						foreach (NetConnection player in server.Connections)
						{
							// ... send information about every other player (actually including self)
							foreach (NetConnection otherPlayer in server.Connections)
							{
								// send position update about 'otherPlayer' to 'player'
								NetOutgoingMessage om = server.CreateMessage();

								// write who this position is for
								om.Write(otherPlayer.RemoteUniqueIdentifier);

								if (otherPlayer.Tag == null)
									otherPlayer.Tag = new int[2];

								int[] pos = otherPlayer.Tag as int[];
								om.Write(pos[0]);
								om.Write(pos[1]);

								// send message
								server.SendMessage(om, player, NetDeliveryMethod.Unreliable);
							}
						}

						// schedule next update
						nextSendUpdates += (1.0 / 30.0);
					}*/
				}

				// sleep to allow other processes to run smoothly
				Thread.Sleep(1);
			}

		}
	}
}
