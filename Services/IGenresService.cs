namespace Movies.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetByID(byte id);
        Task<Genre> AddGenre(Genre genre);
        Genre UpdateGenre(Genre genre);
        Genre DeleteGenre(Genre genre);
        Task<bool> IsValiedGenre(byte id);
        
    }
}
