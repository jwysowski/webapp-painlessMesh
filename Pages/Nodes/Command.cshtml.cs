#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Listener.Models;
using MessageType;
using MessageTypes;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MeshApp.Pages.Nodes
{
    public class CommandModel : PageModel
    {
        private readonly MeshAppContext _context;
        private readonly MessageTypesLUT _typesLUT;
        private readonly MqttFactory _factory;

        public CommandModel(MeshAppContext context)
        {
            _context = context;
            _typesLUT = new MessageTypesLUT();
            _factory = new MqttFactory();
        }

        public IList<Node> Nodes { get; set; }
        public IList<SelectListItem> NodeIds { get; set; }
        public IList<SelectListItem> Types { get; set; } = new List<SelectListItem> {
            new SelectListItem { Value = "Temperature", Text = "Temperature" },
            new SelectListItem { Value = "Humidity", Text = "Humidity" }
        };

        [BindProperty]
        public string TargetType { get; set; }

        [BindProperty]
        public string TargetNodeId { get; set; }

        [BindProperty]
        public double TargetValue { get; set; }

        [BindProperty]
        public bool TargetInManualMode { get; set; }

        public void OnGet()
        {
            GetData();
        }

        public async Task OnPostCommand()
        {
            string message = GetMessage();
            var clientOptions = new MqttClientOptionsBuilder()
                .WithClientId("TestCommandClient")
                .WithTcpServer("localhost", 1883)
                .Build();

            var client = _factory.CreateMqttClient();
            await client.ConnectAsync(clientOptions, CancellationToken.None);
            if (!client.IsConnected)
            {
                GetData();
                return;
            }

            var mqttMessage = new MqttApplicationMessageBuilder()
                                .WithTopic("gate/command")
                                .WithPayload(message)
                                .Build();

            await client.PublishAsync(mqttMessage);
            GetData();
        }

        private string GetMessage()
        {
            string message = ":";
            var node = _context.Node.FirstOrDefault(n => n.NodeId == TargetNodeId);
            if (node == null)
                return string.Empty;

            bool isTemperature = TargetType == "Temperature" ? true : false;
            double currentValue = isTemperature ?
                                node.Temperature
                                : node.Humidity;

            bool isRising = isTemperature ?
                                currentValue > node.Temperature
                                : currentValue > node.Humidity;

            char msgType = _typesLUT.GetMessageType(
                new MsgType(isTemperature, TargetInManualMode, isRising).GetString());

            message.Append(msgType);
            message = message + TargetValue.ToString();
            message = message + TargetNodeId;
            message = message + Checksum(message);
            message = message + System.Environment.NewLine;

            return message;
        }

        private string Checksum(string msg)
        {
            var sum = msg.Sum(c => c);
            return sum.ToString("x");
        }

        private void GetData()
        {
            Nodes = _context.Node.ToList();
            NodeIds = Nodes.Select(n => new SelectListItem
            {
                Value = n.NodeId,
                Text = n.NodeId
            }).ToList();
        }
    }
}
