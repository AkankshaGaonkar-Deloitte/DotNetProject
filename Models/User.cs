
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using dotnetproject.Models;

namespace dotnetproject.Models;

public class User{public User(){
        this.Roles = new HashSet<Roles>();
    }

    [Key]
    public int userid { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    [JsonIgnore]
    public string Password{get;set;}
    public string Email{get;set;}
    [JsonIgnore]
    public virtual ICollection<Project> Projects{get;set;}
    [JsonIgnore]
    [InverseProperty("Reporter")]
    public virtual ICollection<Issue> IssuesCreated{get;set;}
    [JsonIgnore]
    [InverseProperty("Assignee")]
    public virtual ICollection<Issue> IssuesAssigned{get;set;}
    [JsonIgnore]
    public virtual ICollection<Roles> Roles{get;set;}
}