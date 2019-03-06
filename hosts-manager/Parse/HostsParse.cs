using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace hosts_manager.Parse
{
    public class HostsParse
    {
        public static readonly string HOSTS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.System)+ @"\drivers\etc\hosts";

        public static bool HostsExists()
        {
            return File.Exists(HOSTS_PATH);
        }

        public static List<Host> ParseList()
        {
            List<Host> rules = new List<Host>();
            using (StreamReader streamReader = new StreamReader(HOSTS_PATH, Encoding.UTF8))
            {
                string line = "";
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if(line != string.Empty && line[0] != '#')
                    {
                        Match mm = Regex.Match(line, @"^(\d+\.\d+\.\d+\.\d+)\s+(.+)$");
                        if(mm.Success)
                        {
                            Host host = new Host();
                            host.IP = mm.Groups[1].Value;
                            host.Domain = mm.Groups[2].Value;
                            rules.Add(host);
                        }
                    }
                }
            }
            return rules;
        }

        public static string ListToString(List<Host> hosts)
        {
            string result = "";
            foreach(Host host in hosts)
            {
                result += string.Format("{0}\t{1}\r\n", host.IP, host.Domain);
            }
            return result;
        }

        public static void ReWrite(string new_string)
        {
            using (StreamWriter streamWriter = new StreamWriter(HOSTS_PATH, false, Encoding.UTF8))
            {
                streamWriter.Write(new_string);
            }
        }
    }
}
