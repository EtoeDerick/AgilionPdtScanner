using AgilionPdtScanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilionPdtScanner.DTOs
{
    public class OrderListDto : BaseViewModel
    {
        private string? _number;
        private string? _name;
        private DateTime _date;
        private string? _phonenumber;
        private int _itemsCount;
        private float _quantityScanned;
        private float _quantityScannedPercentage;
        private int numbering;

        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }


        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }
        public string Phonenumber
        {
            get => _phonenumber;
            set => SetProperty(ref _phonenumber, value);
        }

        public int ItemsCount 
        { get => _itemsCount;
          set => SetProperty(ref _itemsCount, value);
        }

        public float QuantityScanned 
        { 
            get => _quantityScanned; 
            set => SetProperty(ref _quantityScanned, value);
        }
        public float QuantityScannedPercentage
        {
            get => _quantityScannedPercentage;
            set => SetProperty(ref _quantityScannedPercentage, value);
        }
        public int Numbering { get => numbering; set =>SetProperty(ref numbering, value); }
    }
}
