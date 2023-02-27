using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetproject.Models;
public class IssueDTO
    {
        public string IssueTittle {
            get;
            set;
        }
        public string IssueDescription {
            get;
            set;
        }
        public string IssueType { get; set; }

        public int ?ReporterId { get; set; }

        public string Status{get; set;}
        
        // [JsonIgnore]
        public int projectId{
            get;
            set;
            
        }

    }