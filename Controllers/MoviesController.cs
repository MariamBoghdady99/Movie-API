namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;
        public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _mapper = mapper;
        }

        private new List<string> _allowedExtensions = new List<string> {".jpg", ".png"};
        private long _maxAllowedPosterSize = 1048576;

        [HttpGet]
        public async Task<IActionResult> GetAllDataAsync()
        {
            var movie = await _moviesService.GetAll();

            var data = _mapper.Map<IEnumerable<MovirsDetailsDto>>(movie);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var movie = await _moviesService.GetByID(id);

            if (movie == null)
                return NotFound($"There is no movie with ID: {id}");

            var dto = _mapper.Map<MovirsDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsyns(byte genreId)
        {
            var movie = await _moviesService.GetAll(genreId);

            var data = _mapper.Map<IEnumerable<MovirsDetailsDto>>(movie);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required");

            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed !!");

            if(dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Only 1MB file size is allowed !!");

            var isValiedGenre = await _genresService.IsValiedGenre(dto.GenreId);

            if (!isValiedGenre)
                return BadRequest("Invalied Genre ID !!");

            using var dataStream  = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();

            await _moviesService.CreateAsync(movie);

            return Ok(movie);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm]  MovieDto dto)
        {
            var movie = await _moviesService.GetByID(id);
            if (movie == null)
                return NotFound($"There is no movie with ID: {id}");

            var isValiedGenre = await _genresService.IsValiedGenre(dto.GenreId);
            if (!isValiedGenre)
                return BadRequest("Invalied Genre ID !!");

            if(dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed !!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Only 1MB file size is allowed !!");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoreLine = dto.StoreLine;
            movie.GenreId = dto.GenreId;

            _moviesService.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetByID(id);

            if(movie == null)
                return NotFound($"There is no movie with ID: {id}");

            _moviesService.Delete(movie);

            return Ok(movie);
        }
    }
}
