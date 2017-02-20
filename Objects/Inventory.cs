using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RickAndMortyDataBase
{
    public class Parasite
    {
        private int _id;
        private string _name;

        public Parasite(string name, int id = 0)
        {
            _id = id;
            _name = name;
        }

        public override bool Equals(System.Object otherParasite)
        {
            if(!(otherParasite is Parasite))
            {
                return false;
            }
            else
            {
                Parasite newParasite = (Parasite) otherParasite;
                bool idEquality = (this.GetId() == newParasite.GetId());
                bool nameEquality = (this.GetName() == newParasite.GetName());
                return (idEquality && nameEquality);
            }
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public static List<Parasite> GetAll()
        {
            List<Parasite> allParasites = new List<Parasite>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM parasites;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int parasiteId = rdr.GetInt32(0);
                string parasiteName = rdr.GetString(1);
                Parasite newParasite = new Parasite(parasiteName, parasiteId);
                allParasites.Add(newParasite);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }

            return allParasites;
        }
    }
}
