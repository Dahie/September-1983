GameClient:
***********


Konstruktor:

            NetPeerConfiguration config = new NetPeerConfiguration("xnaapp");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            client = new NetClient(config);
            client.Start();

initialize:
            client.DiscoverLocalPeers(14242);


Update:

{
                    case NetIncomingMessageType.DiscoveryResponse:
                        // just connect to first server discovered
                        client.Connect(msg.SenderEndpoint);
                        interpreter.WriteLine("Connection to missile launch system active. Launch key 1111111 accepted successfully");
                        interpreter.Prompt();
                        break;

                    case NetIncomingMessageType.Data:
                        // receive game map
                        String responseString = msg.ReadString();
                        
                        // write responseString to XNAConsole
                        interpreter.WriteLine(responseString);
                        interpreter.Prompt();
                        break;
                }


sendSequenceName:

                NetOutgoingMessage om = client.CreateMessage();
                om.Write(sequenceName);
                om.Write(round);
                round++;
                client.SendMessage(om, NetDeliveryMethod.Unreliable);


ProgramServer:
**************

init server:
		   NetPeerConfiguration config = new NetPeerConfiguration("xnaapp");
		   Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
		   Config.Port = 14242;

		   // create and start server
		   NetServer server = new NetServer(config);


start server:
			server.Start();

server response implementation: 

                            NetOutgoingMessage om = server.CreateMessage();
                            om.Write(responseString);
                            // send to every player, which is only one in our case
                            foreach (NetConnection player in server.Connections)
                            {
                                server.SendMessage(om, player, NetDeliveryMethod.Unreliable);
                            }