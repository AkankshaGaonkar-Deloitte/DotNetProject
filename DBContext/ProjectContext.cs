using dotnetproject.Models;
using Microsoft.EntityFrameworkCore;
using System;  
using System.Collections.Generic;   
using System.Linq;


namespace dotnetproject;

public class ProjectContext : DbContext
{
    public DbSet<Project> Project { get; set;}
    public DbSet<Issue> Issue {get; set;}
    public DbSet<User> user {get; set;}
    public DbSet<Roles> roles{get; set;}
    public DbSet<Label> labels{get; set;}


    public ProjectContext(DbContextOptions options): base(options){
        
    }

}