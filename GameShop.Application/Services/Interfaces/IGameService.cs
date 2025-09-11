using GameShop.Application.Dtos.GameDtos;

namespace GameShop.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> CreateGameAsync(AddGameDto AddGameDto);
        Task<GameDto> GetGameByIdAsync(Guid id);
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<bool> UpdateGameAsync(UpdateGameDto updateGameDto);
        Task<bool> DeleteGameAsync(Guid id);
    }
}