using SampleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;

namespace SampleWebAPI.Controllers
{
    public class PersonController : ApiController
    {
        string connectionstring = "Data Source=LAPTOP-7UIHE5VR\\SQLEXPRESS;Initial Catalog=PersonDetails;Integrated Security=True";
        static List<Person> persons = new List<Person>();
        public string GetSayHello(string name)
        {
            return ("hello " + name);
        }
        public Person GetPerson()
        {
            Person person = new Person { ID = 1, Name = "Sruthy", Description = "Girl" };
            return person;
        }
        public Person GetPersonList(int id)
        {
            //Person person1 = new Person { ID = 1, Name = "Sruthy", Description = "Female" };
            //persons.Add(person1);
            //Person person2 = new Person { ID = 2, Name = "Kiran", Description = "male" };
            //persons.Add(person2);
            //Person person3 = new Person { ID = 3, Name = "Hari", Description = "Male" };
            //persons.Add(person3);
            //Person person4 = new Person { ID = 4, Name = "Keerthi", Description = "Female" };
            //persons.Add(person4);
            return persons.Find(x => x.ID == id);
        }
        
        public List<Person> GetPersonInsert(string name, string description)
        {
            Person person = new Person();
            SqlConnection sqlconnection = new SqlConnection(connectionstring);
            sqlconnection.Open();
            SqlCommand command = new SqlCommand("PersonSave", sqlconnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Description", description);
            persons.Add(person);
            command.ExecuteNonQuery();
            return persons;
        } 
        public List<Person> GetPersonDetails()
        {
            SqlConnection sqlconnection = new SqlConnection(connectionstring);
            sqlconnection.Open();
            SqlCommand listcommand = new SqlCommand("PersonList", sqlconnection);
            listcommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader reader = listcommand.ExecuteReader();
            while (reader.Read())
            {
                Person person1 = new Person();
                person1.ID = Convert.ToInt32(reader["ID"]);
                person1.Name = Convert.ToString(reader["Name"]);
                person1.Description = Convert.ToString(reader["Description"]);
                persons.Add(person1);
            }
            return persons;
        }
    }
}

