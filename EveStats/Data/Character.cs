using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStats.Data
{
    public class Character
    {
        private string name;
        private string description;
        private string corporation;
        private string location;
        private Dictionary<string, int> ratings = new();

        private Dictionary<string, Dictionary<string, int>> standings = new Dictionary<string, Dictionary<string, int>>
        {
            "Player",
            new Dictionary<string, int>
            {
                "test", 2
            }
        };

        protected string Name
        {
            get { return name; }
            set { name = value; }
        }
        protected string Description
        {
            get { return description; }
            set { description = value; }
        }
        protected string Corporation
        {
            get { return corporation; }
            set { corporation = value; }
        }
        protected string Location
        {
            get { return location; }
            set { location = value; }
        }
        protected Dictionary<string,int> Standings
        {
            get { return standings; }
            set { standings = value; }
        }

        public Character()
        {
            Name = "Uninitialized";
            Description = "Uninitialized";
            Corporation = "Uninitialized";
            Location = "Uninitialized";
        }

        public Character(string name, string description, string corporation, string location, Dictionary<string, int> standings)
        {
            Name = name;
            Description = description;
            Corporation = corporation;
            Location = location;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetDescription(string description)
        {
            this.Description = description;
        }

        public void SetCorporation(string corporation)
        {
            this.Corporation = corporation;
        }

        public void SetLocation(string location)
        {
            this.Location = location;
        }

        public void UpdateStandings(string name, int rating)
        {
            this.Ratings.Add(name, rating);
        }
    }
}
