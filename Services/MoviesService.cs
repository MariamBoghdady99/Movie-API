
namespace Movies.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();

            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreID = 0)
        {
            return await _context.Movies
                .Where(m =>m.GenreId == genreID || genreID == 0)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie> GetByID(int id)
        {
            return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
           _context.Update(movie);
           _context.SaveChanges();

            return movie;
        }
    }
}
