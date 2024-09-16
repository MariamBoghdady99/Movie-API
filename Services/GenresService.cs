namespace Movies.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;
        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Genre> AddGenre(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public Genre UpdateGenre(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<Genre> GetByID(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> IsValiedGenre(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
