﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpServer
{
    public class Server
    {
        public static async void AcceptMessage()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            using UdpClient udpClient = new UdpClient(35487);
            Console.WriteLine("Сервер запущен");

            while(true)
            {
                byte[] buffer = udpClient.Receive(ref ep);
                string data = Encoding.UTF8.GetString(buffer);

                Message message = Message.FromJSON(data);
                if (message.Command == "Send")
                {
                    string IpFromMod = ModIP(message.IpFrom);
                    string IpToMod = ModIP(message.IpTo);

                    string fileName = GenerateFilenAME(IpFromMod, IpToMod);
                    if (fileName != null)
                    {
                        var curDir = Directory.GetCurrentDirectory();
                        curDir = curDir.Replace("\\", "/");
                        string newFileName = string.Format($"{curFir}/history/{fileName}.txt")
                        using var sw = new StreamWriter(newFileName, true);
                        await 
                    }
                    Console.WriteLine(message.ToJSON());
                }
            }
        }

        public static string GenerateFilenAME(string firstIp, string secondIp)
        {

            // 192.168.64.548
            // 192.168.647.45
            
            string[] arr1 = firstIp.Split('_');
            string[] arr2 = secondIp.Split('_');
            for (int i = 0; i < 4; i++)
            {
                if (int.Parse(arr1[i]) > int.Parse(arr2[i]))
                {
                    return secondIp + "=" + firstIp;

                }
                else if (int.Parse(arr1[i]) < int.Parse(arr2[i]))
                {
                    return firstIp + "=" + secondIp;

                }

            }
            return "";

                
        }


        public static string ModIP(string adres)
        {
            adres = adres.Replace(".", "_");
            string modIp = "";
            for (int i = 0; i < adres.Length; i++)
            {
                if (adres[i] == ':')
                {
                    modIp = adres.Substring(0, i);
                }
            }
            if (modIp == "")
            {
                modIp = adres;
            }
            return modIp;
        }
      
    }
}
