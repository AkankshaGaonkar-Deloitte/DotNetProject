using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetproject.Models{
public class Issue
    {
        [Key]
        public int IssueId {
            get;
            set;
        }

        public string IssueTittle {
            get;
            set;
        }
        public string IssueDescription {
            get;
            set;
        }
        public int projectId{
            get;
            set;
        }
        public virtual User ?Reporter{get;set;}
        public virtual User? Assignee {get;set;}
        public string Status{get;set;}
        public string IssueType{get;set;}

        [JsonIgnore]
        //navigation properties
        public Project project{
            get;
            set;
        }
        public virtual ICollection<Label>? Labels{get;set;}

        // public ICollection<Label> labels{
        //     get;
        //     set;
        // }
    }
}