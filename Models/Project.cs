using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetproject.Models;
public class Project
    {
        [Key]
        public int ProjectId {
            get;
            set;
        }
        public string ProjectName {
            get;
            set;
        }
        public string ProjectDescription {
            get;
            set;
        }
        // [JsonIgnore]
        //navigation properties
        public ICollection<Issue> issues{
            get;
            set;
        }
        public  User Creator{get;set;}

}