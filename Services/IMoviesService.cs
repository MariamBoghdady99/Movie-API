namespace Movies.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte genreID = 0);
        Task<Movie> GetByID(int id);
        Task<Movie> CreateAsync(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);

    }
}
