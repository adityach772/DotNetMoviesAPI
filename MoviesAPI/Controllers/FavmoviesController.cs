using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.Data;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        private readonly searchmoviesContext _context;

        public FavoriteMoviesController(searchmoviesContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavMovie>>> GetFavoriteMovies()
        {
            try
            {
                // Fetch all favorite movies
                var favoriteMovies = await _context.FavMovies.ToListAsync();

                // If no favorite movies found, return 404
                if (favoriteMovies == null || favoriteMovies.Count == 0)
                {
                    return NotFound();
                }

                return favoriteMovies;
            }
            catch (Exception ex)
            {
                // If an error occurs, return 500 with error message
                return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching favorite movies: " + ex.Message);
            }
        }


        // GET: api/FavoriteMovies/5
        [HttpGet("{id}")]
        public ActionResult<FavMovie> GetFavoriteMovies(int id)
        {
            // Find favorite movie by ID
            var favoriteMovies = _context.FavMovies.Find(id);

            // If not found, return 404
            if (favoriteMovies == null)
            {
                return NotFound();
            }

            return favoriteMovies;
        }

        // POST: api/FavoriteMovies
        [HttpPost]
        public ActionResult<FavMovie> PostFavoriteMovies(FavMovie favoriteMovies)
        {
            // Add new favorite movie
            _context.FavMovies.Add(favoriteMovies);
            _context.SaveChanges();

            // Return 201 with the created resource
            return CreatedAtAction(nameof(GetFavoriteMovies), new { id = favoriteMovies.ID }, favoriteMovies);
        }

        // PUT: api/FavoriteMovies
        [HttpPut]
        public async Task<IActionResult> PutFavoriteMovies(FavMovie favoriteMovie)
        {
            // Find existing favorite movie
            var existingMovie = await _context.FavMovies.FindAsync(favoriteMovie.ID);
            if (existingMovie == null)
            {
                return NotFound($"Favorite movie with ID {favoriteMovie.ID} not found.");
            }

            // Update movie details
            existingMovie.MovieName = favoriteMovie.MovieName;
            existingMovie.Actor = favoriteMovie.Actor;
            existingMovie.Director = favoriteMovie.Director;

            try
            {
                // Save changes
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                if (!FavoriteMoviesExists(favoriteMovie.ID))
                {
                    return NotFound($"Favorite movie with ID {favoriteMovie.ID} not found.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error updating favorite movie.");
                }
            }
        }


        // DELETE: api/FavoriteMovies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFavoriteMovies(int id)
        {
            // Find favorite movie by ID
            var favoriteMovies = _context.FavMovies.Find(id);

            // If not found, return 404
            if (favoriteMovies == null)
            {
                return NotFound();
            }

            // Remove favorite movie
            _context.FavMovies.Remove(favoriteMovies);
            _context.SaveChanges();

            return NoContent();
        }

        // Check if favorite movie exists
        private bool FavoriteMoviesExists(int id)
        {
            return _context.FavMovies.Any(e => e.ID == id);
        }
    }
}
