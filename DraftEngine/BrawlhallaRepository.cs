using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine
{
    using System.Configuration;
    using System.Data.SQLite;
    using System.IO;
    using System.Threading;

    using DraftEngine.Interfaces;
    using DraftEngine.Models;

    public class BrawlhallaRepository : ICharacterRepository
    {
        public BrawlhallaRepository()
        {
            using (var sqlConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["BrawlhallaConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                var cmd = sqlConnection.CreateCommand();
                var sb = new StringBuilder();
                var fileLines = File.ReadLines(@"C:\Users\fas1175\Downloads\brawl.txt");

                foreach (var fileLine in fileLines)
                {
                    var name = fileLine.Split('^')[0];
                    var icon = fileLine.Split('^')[1];
                    sb.AppendLine($"('{name}', '{icon}'),");
                }

                cmd.CommandText = $@"DELETE FROM Hero; INSERT INTO Hero (Name, Icon) VALUES {sb.ToString().TrimEnd(new []{ ',', '\n','\r'})}";

                var reader = cmd.ExecuteNonQuery();

                //while (reader.Read())
                //{
                //    var name = reader["Name"].ToString();
                //    var icon = reader["Icon"].ToString();
                //    characterList.Add(new Hero(name) { Icon = icon });
                //}
            }
        }

        public IEnumerable<ICharacter> GetAllCharacters()
        {
            var characterList = new List<Hero>();
            
            using (var sqlConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["BrawlhallaConnectionString"].ConnectionString))
            {
                sqlConnection.Open();
                var cmd = sqlConnection.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Hero";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader["Name"].ToString();
                    var icon = reader["Icon"].ToString();
                    characterList.Add(new Hero(name) { Icon = icon });
                }
            }

            return characterList;
        }
    }
}
