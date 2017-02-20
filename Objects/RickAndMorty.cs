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

        public void SetName(string newName)
        {
            _name = newName;
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO parasites (name) OUTPUT INSERTED.id VALUES (@ParasiteName);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@ParasiteName";
            nameParameter.Value = this.GetName();
            cmd.Parameters.Add(nameParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }

        public static Parasite Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM parasites WHERE id = @ParasiteId;", conn);
            SqlParameter parasiteIdParameter = new SqlParameter();
            parasiteIdParameter.ParameterName = "@ParasiteId";
            parasiteIdParameter.Value = id.ToString();
            cmd.Parameters.Add(parasiteIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundParasiteId = 0;
            string foundParasiteName = null;
            while(rdr.Read())
            {
                foundParasiteId = rdr.GetInt32(0);
                foundParasiteName = rdr.GetString(1);
            }
            Parasite foundParasite = new Parasite(foundParasiteName, foundParasiteId);
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundParasite;
        }

        public static void DeleteAll()
          {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM parasites;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
          }
    }
}
