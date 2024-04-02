namespace cs_tunering.Tournaments
{
    public static class TournamentsModule
    {
        public static void TournamentsEndpoints(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/tournaments", GetTournament);
            app.MapGet("/tournaments/{id}", GetTournamentById);
            app.MapGet("/tournaments/child", GetChildren);
            app.MapPost("/tournaments", AddTournament);
            app.MapPost("/tournaments/{parentId}/child", AddChildTournament);
            app.MapPut("/tournaments/{id}", UpdateTournament);
            app.MapDelete("/tournaments/{id}", DeleteTournament);
        }

        /// <summary>
        /// Retrieves all main/parent tournaments
        /// </summary>
        public static async Task<IResult> GetTournament(DataContext context)
        {
            var tournaments = await context.Tournaments.ToListAsync();

            if (tournaments.Any() == false) 
                return Results.NotFound("No tournaments found.");

            return Results.Ok(tournaments);
        }

        /// <summary>
        /// Retrieves all child tournaments
        /// </summary>
        public static async Task<IResult> GetChildren(DataContext context)
        {
            var tournaments = await context.ChildTournaments.ToListAsync();

            if (tournaments.Any() == false)
                return Results.NotFound("No tournaments found.");

            return Results.Ok(tournaments);
        }

        /// <summary>
        /// Retrieves specific tournament information by unique id
        /// </summary>
        public static async Task<IResult> GetTournamentById(DataContext context, int id)
        {
            var tournament = await context.Tournaments
                .Include(t => t.ChildTournaments) 
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null)
                return Results.NotFound($"No tournament found matching Id: {id}");

            tournament.ChildTournaments.Clear();

            var childTournamentsList = await context.ChildTournaments
                .Where(ct => ct.ParentTournamentId == id)
                .ToListAsync();
            
            if (childTournamentsList.Any() == false)
                return Results.Ok(tournament);

            foreach (var child in childTournamentsList)
                tournament.ChildTournaments.Add(child);

            return Results.Ok(tournament);
        }

        /// <summary>
        /// Adds a new tournament
        /// </summary>
        public static async Task<IResult> AddTournament(DataContext context, Tournament tournament)
        {
            context.Tournaments.Add(tournament);
            await context.SaveChangesAsync();
            return Results.Created($"/tournament/{tournament.Id}", tournament);
        }

        /// <summary>
        /// Add a child (tournament) to a tournament
        /// </summary>
        public static async Task<IResult> AddChildTournament(DataContext context, int parentId, Tournament childTournament)
        {
            var parentTournament = await context.Tournaments.FindAsync(parentId);
            if (parentTournament == null)
                return Results.NotFound("Parent tournament not found.");

            childTournament.IsParentTournament = false;

            var tournament = new ChildTournament
            {
                Name = childTournament.Name,
                PlayerCount = childTournament.PlayerCount,
                ParentTournamentId = parentId,
                ParentTournament = parentTournament
            };

            context.ChildTournaments.Add(tournament);
            await context.SaveChangesAsync();

            return Results.Created($"/tournament/{parentTournament.Id}/child/{childTournament.Id}", childTournament);
        }

        /// <summary>
        /// Updates a specified tournament 
        /// </summary>
        public static async Task<IResult> UpdateTournament(DataContext context, Tournament updatedTournament, int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);
            if (tournament == null)
                return Results.NotFound("This tournament doesn't exist.");

            tournament.Name = updatedTournament.Name;
            tournament.PlayerCount = updatedTournament.PlayerCount;
            tournament.ChildTournaments = updatedTournament.ChildTournaments;
            tournament.IsParentTournament = updatedTournament.IsParentTournament;
            await context.SaveChangesAsync();

            return Results.Created($"/tournament/{tournament.Id}", tournament);
        }

        /// <summary>
        /// Delete a tournament
        /// </summary>
        public static async Task<IResult> DeleteTournament(DataContext context, int id)
        {
            var tournament = await context.Tournaments.FindAsync(id);
            if (tournament is null)
                return Results.NotFound("This tournament doesn't exist.");

            context.Tournaments.Remove(tournament);
            await context.SaveChangesAsync();

            return Results.Ok(tournament);
        }

    }
}
