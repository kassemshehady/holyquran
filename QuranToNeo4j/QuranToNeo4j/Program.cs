using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace QuranToNeo4j
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlnewo = "http://localhost:7474/db/data/";
            var user = "neo4j";
            var password = "123456";
            var client = new GraphClient(new Uri(urlnewo), user, password);
            client.Connect();

            var lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"quran-clean.txt");
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                    return;
                var l = line.Split('|');
                var sora = l[0];
                var aya = l[1];
                var words = l[2].Trim().Split(' ').ToArray();
                for (int i = 0; i < words.Length; i++)
                {
                    if (string.IsNullOrEmpty(words[i]))
                        continue;

                    Console.WriteLine(sora + " " + aya + " " + i);

                    var nextword = "";
                    if (i + 1 == words.Length)
                        nextword = sora + "-" + aya;
                    else
                        nextword = words[i + 1];

                    var dic = new Dictionary<string, object> { { "firstword", words[i] }, { "nextword", nextword } };
                    var query = new CypherQuery(
                            "MERGE (w1:Word {title: {firstword}}) " +
                            "MERGE (w2:Word {title: {nextword}}) " +
                            "MERGE (w1)-[r:NEXT]->(w2) " +
                            "On Create set r.sora= " + sora + ", r.aya = " + aya + ",r.index = " + (i + 1)

                            , dic, CypherResultMode.Projection);

                    ((IRawGraphClient)client).ExecuteCypher(query);
                }
            }


        }
    }
}
