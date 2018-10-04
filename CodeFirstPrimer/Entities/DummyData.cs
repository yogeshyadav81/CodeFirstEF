using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirstPrimer.Models.NHL;
namespace CodeFirstPrimer.Entities
{
    public class DummyData
    {
        public static List<Team> getTeams()
        {
            List<Team> teams = new List<Team>()
            {
                new Team() {TeamName="SuperKing",City="Panjab",Province="PB" },
                new Team() {TeamName="DelhiKing",City="Delhi",Province="DL" },
                new Team() {TeamName="ChannaiKing",City="Channai",Province="CH" },
            };

            return teams;
       }

        public static List<Player> getPlayers(NhlContext context)
        {
            List<Player> Players = new List<Player>()
            {
                new Player() {FirstName="Yogesh",LastName="Yadav",Position="forward",TeamName = context.Teams.Find("SuperKing").TeamName },
                new Player() {FirstName="Rohan",LastName="Sharma",Position="Back",TeamName = context.Teams.Find("DelhiKing").TeamName },
                new Player() {FirstName="Raj",LastName="Kumar",Position="Top",TeamName = context.Teams.Find("ChannaiKing").TeamName }
            };

            return Players;
        }
    }
}