using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace dotnetproject.Models;
public class ProjectDTO
    {
        public string ProjectName {
            get;
            set;
        }
        public string ProjectDescription {
            get;
            set;
        }
        public int CreatorId {
            get;
            set;
        }



    }