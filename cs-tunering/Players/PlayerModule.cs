using cs_tunering;
using Microsoft.AspNetCore.Builder;

namespace cs_tunering.Players
{
    public static class PlayerModule
    {
        public static void PlayersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/players", GetPlayer);
            app.MapGet("/players/{id}", GetPlayerById);
            app.MapPost("/players", AddPlayer);
            app.MapPut("/players/{id}", UpdatePlayer);
            app.MapDelete("/players/{id}", DeletePlayer);

        }

        /// <summary>
        /// Retrieves all players
        /// </summary>
        public static async Task<IResult> GetPlayer(DataContext context)
        {
            var player = await context.Players.ToListAsync();
            return Results.Ok(player);
        }

        /// <summary>
        /// Retrieves specific player by unique id
        /// </summary>
        public static async Task<IResult> GetPlayerById(DataContext context, int id)
        {
            var player = await context.Players.FindAsync(id);

            if (player == null)
                return Results.NotFound($"No player found matching id: {id}");

            return Results.Ok(player);
        }

        /// <summary>
        /// Adds a new player 
        /// </summary>
        public static async Task<IResult> AddPlayer(DataContext context, Player player)
        {
            context.Players.Add(player);
            await context.SaveChangesAsync();
            return Results.Created($"/players/{player.Id}", player);
        }

        /// <summary>
        /// Updates a specified player
        /// </summary>
        public static async Task<IResult> UpdatePlayer(DataContext context, Player updatedPlayer, int id)
        {
            var player = await context.Players.FindAsync(id);
            if (player == null)
                return Results.NotFound("This player doesn't exist.");

            player.Name = updatedPlayer.Name;
            player.Age = updatedPlayer.Age;
            player.TournamentId = updatedPlayer.TournamentId;
            await context.SaveChangesAsync();

            return Results.Created($"/players/{player.Id}", player);
        }

        /// <summary>
        /// Deletes specified player
        /// </summary>
        public static async Task<IResult> DeletePlayer(DataContext context, int id)
        {
            var player = await context.Players.FindAsync(id);
            if (player is null)
                return Results.NotFound("This player doesn't exist.");

            context.Players.Remove(player);
            await context.SaveChangesAsync();

            return Results.Created($"/players/{player.Id}", player);
        }

    }
}
