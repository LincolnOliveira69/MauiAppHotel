using MauiAppHotel.Model;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current;

        // Cria uma lista com o item de orientação
        var listaComOrientacao = new List<Quarto>
        {
            new Quarto { Descricao = "Selecione a Suíte" } // item fictício
        };

        listaComOrientacao.AddRange(PropriedadesApp.lista_quartos);

        // Preenche o Picker com a nova lista
        pck_quarto.ItemsSource = listaComOrientacao;
        pck_quarto.SelectedIndex = 0;

        // Define limites para datas de check-in e check-out
        dtpck_checkin.MinimumDate = DateTime.Now;
        dtpck_checkin.MaximumDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day);

        dtpck_checkout.MinimumDate = dtpck_checkin.Date.AddDays(1);
        dtpck_checkout.MaximumDate = dtpck_checkin.Date.AddMonths(6);
    }

    private async void OnSobreTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PaginaSobre());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var suiteSelecionada = (Quarto)pck_quarto.SelectedItem;
            int adultos = (int)stp_adultos.Value;
            int criancas = (int)stp_criancas.Value;

            // Validação da suíte
            if (suiteSelecionada == null || suiteSelecionada.Descricao == "Selecione a Suíte")
            {
                await DisplayAlert("Atenção", "Por favor, selecione uma suíte válida.", "OK");
                return;
            }

            // Validação de hóspedes
            if (adultos == 0 && criancas == 0)
            {
                await DisplayAlert("Atenção", "A reserva precisa ter pelo menos um hóspede.", "OK");
                return;
            }

            // Crianças não podem se hospedar sozinhas
            if (adultos == 0 && criancas > 0)
            {
                await DisplayAlert("Atenção", "Crianças não podem se hospedar sem um adulto responsável.", "OK");
                return;
            }

            var hospedagem = new Hospedagem
            {
                Suite = suiteSelecionada.Descricao,
                Adultos = adultos,
                Criancas = criancas,
                Checkin = dtpck_checkin.Date,
                Checkout = dtpck_checkout.Date,
                ValorDiariaAdulto = suiteSelecionada.ValorDiariaAdulto,
                ValorDiariaCrianca = suiteSelecionada.ValorDiariaCrianca,
                ValorTotal = CalcularValorTotal()
            };

            await Navigation.PushAsync(new HospedagemContratada(hospedagem));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private decimal CalcularValorTotal()
    {
        var suiteSelecionada = (Quarto)pck_quarto.SelectedItem;
        if (suiteSelecionada == null || suiteSelecionada.Descricao == "Selecione a Suíte") return 0;

        int dias = (dtpck_checkout.Date - dtpck_checkin.Date).Days;
        int adultos = (int)stp_adultos.Value;
        int criancas = (int)stp_criancas.Value;

        double valorPorDia = (suiteSelecionada.ValorDiariaAdulto * adultos) +
                             (suiteSelecionada.ValorDiariaCrianca * criancas);

        return (decimal)(valorPorDia * dias);
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime dataSelecionadaCheckin = e.NewDate;

        dtpck_checkout.MinimumDate = dataSelecionadaCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataSelecionadaCheckin.AddMonths(6);
    }
}