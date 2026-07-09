using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TableGames.Desktop.Models;
using TableGames.Desktop.Services;

namespace TableGames.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly GameApiClient _api;

    public MainWindowViewModel() : this(new GameApiClient())
    {
    }

    public MainWindowViewModel(GameApiClient api)
    {
        _api = api;
        Games = new ObservableCollection<Game>();
        // Carga inicial al abrir la ventana.
        _ = LoadAsync();
    }

    /// <summary>Lista de juegos mostrada en la tabla.</summary>
    public ObservableCollection<Game> Games { get; }

    [ObservableProperty]
    private Game? _selectedGame;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _statusMessage = "Listo.";

    // --- Campos del formulario de edición ---
    [ObservableProperty]
    private int _editingId; // 0 = nuevo registro

    [ObservableProperty]
    private string _formName = string.Empty;

    [ObservableProperty]
    private string _formCategory = "General";

    [ObservableProperty]
    private int _formMinPlayers = 1;

    [ObservableProperty]
    private int _formMaxPlayers = 4;

    [ObservableProperty]
    private decimal _formPricePerHour;

    [ObservableProperty]
    private bool _formIsAvailable = true;

    public bool IsEditing => EditingId != 0;
    public string FormTitle => IsEditing ? $"Editar juego #{EditingId}" : "Nuevo juego";

    // Al seleccionar una fila, se rellena el formulario.
    partial void OnSelectedGameChanged(Game? value)
    {
        if (value is null) return;
        EditingId = value.Id;
        FormName = value.Name;
        FormCategory = value.Category;
        FormMinPlayers = value.MinPlayers;
        FormMaxPlayers = value.MaxPlayers;
        FormPricePerHour = value.PricePerHour;
        FormIsAvailable = value.IsAvailable;
    }

    partial void OnEditingIdChanged(int value)
    {
        OnPropertyChanged(nameof(IsEditing));
        OnPropertyChanged(nameof(FormTitle));
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        try
        {
            IsBusy = true;
            StatusMessage = "Cargando juegos...";
            var games = await _api.GetGamesAsync();
            Games.Clear();
            foreach (var g in games)
                Games.Add(g);
            StatusMessage = $"{Games.Count} juegos cargados.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al cargar: {ex.Message}. ¿Está corriendo la API?";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void New()
    {
        SelectedGame = null;
        EditingId = 0;
        FormName = string.Empty;
        FormCategory = "General";
        FormMinPlayers = 1;
        FormMaxPlayers = 4;
        FormPricePerHour = 0;
        FormIsAvailable = true;
        StatusMessage = "Formulario listo para un juego nuevo.";
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(FormName))
        {
            StatusMessage = "El nombre es obligatorio.";
            return;
        }
        if (FormMaxPlayers < FormMinPlayers)
        {
            StatusMessage = "El máximo de jugadores no puede ser menor que el mínimo.";
            return;
        }

        var game = new Game
        {
            Id = EditingId,
            Name = FormName.Trim(),
            Category = string.IsNullOrWhiteSpace(FormCategory) ? "General" : FormCategory.Trim(),
            MinPlayers = FormMinPlayers,
            MaxPlayers = FormMaxPlayers,
            PricePerHour = FormPricePerHour,
            IsAvailable = FormIsAvailable
        };

        try
        {
            IsBusy = true;
            if (EditingId == 0)
            {
                await _api.CreateGameAsync(game);
                StatusMessage = $"Juego '{game.Name}' creado.";
            }
            else
            {
                await _api.UpdateGameAsync(game);
                StatusMessage = $"Juego '{game.Name}' actualizado.";
            }
            await LoadAsync();
            New();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedGame is null)
        {
            StatusMessage = "Selecciona un juego para eliminar.";
            return;
        }

        var name = SelectedGame.Name;
        try
        {
            IsBusy = true;
            await _api.DeleteGameAsync(SelectedGame.Id);
            StatusMessage = $"Juego '{name}' eliminado.";
            await LoadAsync();
            New();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error al eliminar: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
