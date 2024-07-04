using AgilionPdtScanner.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilionPdtScanner.DTOs
{
    public class Item : BaseViewModel
    {
        private string? code;
        private int quantity;
        private string? designation;
        private string? statusColor = "Orange";
        private int numbering;

        //[JsonProperty("Code")]
        public string? Code { get => code; set => SetProperty(ref code, value); }

        //[JsonProperty("Quantity")]
        public int Quantity { get => quantity; set => SetProperty(ref quantity, value); }

        //[JsonProperty("Designation")]
        public string? Designation { get => designation; set => SetProperty(ref designation, value); }

        //[JsonProperty("StatusCode")]
        public string? StatusColor { get => statusColor; set => SetProperty(ref statusColor, value); }

        public int Numbering { get => numbering; set => SetProperty(ref numbering, value); }
    }
}
