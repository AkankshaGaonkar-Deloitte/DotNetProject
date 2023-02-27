
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnetproject.Models;

public class Roles{

    public Roles(){
        this.Users = new HashSet<User>();
    }
    

    [Key]
    public int roleid { get; set; }
    public string title { get; set; }


    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; }
}