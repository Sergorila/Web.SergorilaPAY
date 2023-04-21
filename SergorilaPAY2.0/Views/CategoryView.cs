using System.ComponentModel.DataAnnotations;
using AutoMapper;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SergorilaPAY2._0.Views;

namespace SergorilaPAY2._0.Views;

public class CategoryView
{
    public int Id { get; set; }
    
    [Required]
    public string? Title { get; set; }
    
}