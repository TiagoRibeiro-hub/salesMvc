﻿#nullable disable
namespace SalesWebMvc.ViewModels;
public class SalesRecordViewModel
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime Date { get; set; }

    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double Amount { get; set; }
    public SaleStatus Status { get; set; }
    public Seller Seller { get; set; }
}

