﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Spectre.Console;

namespace NetVulnFind
{
    class LoadConfig
    {
        protected static string API_KEY;
        public LoadConfig()
        {

        }
        public static async Task Config()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("config.txt");
                if (File.Exists("config.txt"))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split('=');
                        API_KEY = values[1];
                    }
                }
                else
                {
                    File.Create("config.txt");
                    StreamWriter writer = new StreamWriter("config.txt");
                    writer.Write("API_KEY=");
                    Config(); // Calls the method again to load the API KEY 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an unexpected error {0}", e.Message);
            }
            finally
            {
                reader?.Close();
            }
        }
    }
}