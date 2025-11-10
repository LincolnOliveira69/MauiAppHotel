namespace MauiAppHotel.Model
{
    public class Hospedagem
    {
        public string Suite { get; set; }
        public int Adultos { get; set; }
        public int Criancas { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public decimal ValorTotal { get; set; }
        public double ValorDiariaAdulto { get; set; }
        public double ValorDiariaCrianca { get; set; }

        public int DiasEstadia => (Checkout - Checkin).Days;

        // Datas formatadas para exibição
        public string CheckinFormatado => Checkin.ToString("dd/MM/yyyy");
        public string CheckoutFormatado => Checkout.ToString("dd/MM/yyyy");
    }
}