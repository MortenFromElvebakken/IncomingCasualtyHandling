using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

using System.Windows;
using System.Net.Http;
using System.Diagnostics;


namespace IncomingCasualtyHandling.DAL
{
    public class TestSubScriptionClass
    {
        Subscription testSubscription;
        FhirClient testSubClient;
        //string fhirURL = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";
        
        public TestSubScriptionClass()
        {
            //ws = new WebSocketSharp.WebSocket(fhirURL);

            //setupSubscription(); // er gjordt, subscription id vi fik tilbage er 4952
            //setupWebsocket();
        }
        public void setupSubscription()
        {
            testSubscription = new Hl7.Fhir.Model.Subscription();
            testSubClient = new Hl7.Fhir.Rest.FhirClient("http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3");
            Hl7.Fhir.Model.Subscription.ChannelComponent testChannel = new Subscription.ChannelComponent();
            testChannel.Type = Hl7.Fhir.Model.Subscription.SubscriptionChannelType.Websocket;

            testSubscription.Channel = testChannel;
            Hl7.Fhir.Model.Subscription.SubscriptionStatus testStatus = new Subscription.SubscriptionStatus();
            testStatus = Subscription.SubscriptionStatus.Requested;

            testSubscription.Status = testStatus;
            //testSubscription.Channel.Endpoint = "http://localhost:8080/hapi-fhir-jpaserver-example/baseDstu3";
            //testSubscription.Channel.Payload = "application/xml";
            testSubscription.Criteria = "patient?active=true";
            testSubClient.Create<Subscription>(testSubscription);
            
        }
        //private WebSocket ws;
        
       
        
        //public void setupWebsocket()
        //{
        //    string wsURL = "ws://localhost:8080/hapi-fhir-jpaserver-example/";
        //    ws = new WebSocketSharp.WebSocket(wsURL);
            
        //    ws.EmitOnPing = true;
        //    //ws.SetCredentials("theUsername", "thePassword", true);
        //    //ws.EmitOnPing = true;
        //    ws.EnableRedirection = true;
        //    //ws.Origin = "/*";
            
        //    //var t = ws.Protocol;
        //    ws.Log.Level = LogLevel.Debug;
        //    ws.Connect();
            

        //    ws.OnMessage += Ws_OnMessage;
        //    ws.OnClose += Ws_OnClose;
        //    ws.OnOpen += Ws_OnOpen;
            
        //}

        //private void Ws_OnOpen(object sender, EventArgs e)
        //{
        //    ws.Send("bind 4952");
        //}

        //private void Ws_OnClose(object sender, CloseEventArgs e)
        //{
        //    Debug.WriteLine("ws er lukket");
        //}

        //private void Ws_OnMessage(object sender, MessageEventArgs e)
        //{
        //    if (e.IsPing)
        //    {
        //        //opdater patients??
        //        Debug.WriteLine("Der er ping igennem?");
                
        //    }
        //    else
        //    {
        //        Debug.WriteLine("noget andet igennem" + e.Data.ToString());
        //    }
        //}


        //public HubConnection connection;

        //public IHubProxy HubProxy { get; set; }

        //public void setupWebsocket()
        //{
        //    connection = new HubConnection(fhirURL);

        //    connection.Closed += async (error) =>
        //    {
        //        await Task.Delay(new Random().Next(0, 5) * 1000);
        //        await connection.StartAsync();
        //    };

        //}

        //private void Connection_Closed()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
