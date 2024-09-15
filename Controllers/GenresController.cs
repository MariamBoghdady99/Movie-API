namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genreservice;

        public GenresController(IGenresService genreservice)
        {
            _genreservice = genreservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDataAsync()
        {
            var genres = await _genreservice.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };

            await _genreservice.AddGenre(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto dto)
        {
            var genre = await _genreservice.GetByID(id);

            if(genre == null) 
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = dto.Name;

            _genreservice.UpdateGenre(genre);

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreservice.GetByID(id);

            if (genre == null)
                return NotFound($"No genre was found with ID: {id}");

            _genreservice.DeleteGenre(genre);

            return Ok(genre);
        }
    }
}