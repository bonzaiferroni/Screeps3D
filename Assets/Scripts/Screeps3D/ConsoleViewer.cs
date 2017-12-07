using System;
using System.IO;
using UnityEngine;
using Screeps3D;

namespace Screeps3D
{
    [RequireComponent(typeof(ScreepsAPI))]
    public class ConsoleViewer : MonoBehaviour
    {
        [SerializeField] private UnityConsole _console;
        private ScreepsAPI _api;

        private void Start()
        {
            _api = ScreepsAPI.Instance;
            _api.OnConnectionStatusChange += OnConnectionStatusChange;
            _console.OnInput += OnInput;
            _api.Console.OnConsoleMessage += OnMessage;
            _api.Console.OnConsoleError += OnError;
            _api.Console.OnConsoleResult += OnResult;
            _console._panel.Show(false, true);
        }

        private void OnConnectionStatusChange(bool isConnected)
        {
            _console._panel.Show(isConnected);
        }

        private void OnInput(string obj)
        {
            _api.Console.Input(obj);
        }

        private void OnMessage(string obj)
        {
            PrintMessage(obj, Color.white);
        }

        private void OnError(string obj)
        {
            PrintMessage(obj, Color.red);
        }

        private void OnResult(string obj)
        {
            PrintMessage(obj, Color.green);
        }

        private void PrintMessage(string message, Color color)
        {
            var reader = new StringReader(message);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                _console.AddMessage(line, color);
            }
        }
    }
}