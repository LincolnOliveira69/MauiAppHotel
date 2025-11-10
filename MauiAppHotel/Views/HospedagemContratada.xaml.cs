using MauiAppHotel.Model;

namespace MauiAppHotel.Views;

public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada(Hospedagem hospedagem)
    {
        InitializeComponent();
        BindingContext = hospedagem;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void OnConfirmarReservaClicked(object sender, EventArgs e)
    {
        bool confirmado = await DisplayAlert("Confirmação", "Deseja confirmar a reserva?", "Sim", "Cancelar");

        if (confirmado)
        {
            await DisplayAlert("Reserva Confirmada", "Sua reserva foi registrada com sucesso!", "OK");
            // Aqui você pode salvar a reserva, navegar para outra tela ou limpar os dados
        }
    }
}