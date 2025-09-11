using GameShop.Application.Dtos.GameDtos;
using GameShop.Application.Services.Interfaces;
using GameShop.Domain.Entities;
using GameShop.Domain.Interfaces;

namespace GameShop.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<GameDto> CreateGameAsync(AddGameDto createGameDto)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Name = createGameDto.Name,
                Description = createGameDto.Description,
                Price = createGameDto.Price,
                Platform = createGameDto.Platform,
                Stock = createGameDto.StockLevel,
            };

            await _unitOfWork.Games.AddAsync(game);

            await _unitOfWork.CompleteAsync();

            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                Platform = game.Platform,
                StockLevel = game.Stock
            };
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(id);

            if (game == null)
                return false;


            game.IsDeleted = true;
            _unitOfWork.Games.Update(game);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            var games = await _unitOfWork.Games.GetAllAsync();

            return games.Where(g => !g.IsDeleted).Select(game => new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                Platform = game.Platform,
                StockLevel = game.Stock
            });
        }

        public async Task<GameDto> GetGameByIdAsync(Guid id)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(id);

            if (game == null || game.IsDeleted)
                return null;


            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                Platform = game.Platform,
                StockLevel = game.Stock
            };
        }

        public async Task<bool> UpdateGameAsync(UpdateGameDto updateGameDto)
        {
            var game = await _unitOfWork.Games.GetByIdAsync(updateGameDto.Id);
            if (game == null || game.IsDeleted)
            {
                return false;
            }

            game.Name = updateGameDto.Name;
            game.Description = updateGameDto.Description;
            game.Price = updateGameDto.Price;
            game.Platform = updateGameDto.Platform;
            game.Stock = updateGameDto.StockLevel;

            _unitOfWork.Games.Update(game);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}