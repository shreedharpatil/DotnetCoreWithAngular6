using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        
        public string RRNo { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Mobile { get; set; }

        public string EMMobile { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string District { get; set; }

        public string Taluk { get; set; }

        public string Village { get; set; }

        public string Pincode { get; set; }

        public string Feeder { get; set; }

        public string Transformer { get; set; }
    }
}
