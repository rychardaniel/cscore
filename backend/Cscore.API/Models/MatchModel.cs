using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cscore.API.Models;

public class MatchModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ChampionshipId { get; set; }

    public ChampionshipModel Championship { get; set; }
    
    public TypeMatch TypeMatch { get; set; }
}

public enum TypeMatch
{
    Futsal = 1,
    Volleyball = 2,
    Chess = 3
}