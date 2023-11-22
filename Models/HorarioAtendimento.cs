using Microsoft.CodeAnalysis;
using System;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.Models;

public class HorarioAtendimento
{
	public int HorarioId { get; set; }
	[Required]
	public DateTime DataInicio { get; set; }
    [Required]
    public DateTime DataFim { get; set; }
	public int DeptID { get; set; }


}
