using System;

namespace Pizzaria.Domain
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Sabor { get; set; }
        public string Tamanho { get; set; }
        public decimal Valor { get; set; }
        public bool Borda { get; set; }
        public string ImgURL { get; set; }
    }
}