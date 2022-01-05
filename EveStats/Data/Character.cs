using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStats.Data
{
    public class Character : IEquatable<Character>
    {
        private int character_id;
        private string name;
        private string description;
        private string corporation;
        private string location;

        protected int CharacterId
        {
            get { return character_id; }
            set { character_id = value; }
        }
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

        public Character()
        {
            Name = "Uninitialized";
            Description = "Uninitialized";
            Corporation = "Uninitialized";
            Location = "Uninitialized";
        }

        public Character(string name, string description, string corporation, string location)
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Character objAsCharacter = obj as Character;

            if (objAsCharacter == null) return false;
            else return Equals(objAsCharacter);
        }

        public override int GetHashCode()
        {
            return CharacterId;
        }

        public bool Equals(Character other)
        {
            if (other == null) return false;
            return (this.CharacterId.Equals(other.CharacterId));
        }
    }

    public class PlayerCharacter : Character
    {
        private float wallet_balance;
        private List<string> industry_jobs;

        protected float WalletBalance
        {
            get { return wallet_balance;}
            set { wallet_balance = value; }
        }

        public PlayerCharacter()
        {
            WalletBalance = 0;
        }
    }
}
