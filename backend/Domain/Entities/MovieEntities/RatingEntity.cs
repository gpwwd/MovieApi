using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.MovieEntities
{
    public class RatingEntity
    {
        [JsonIgnore]
        public Guid Id { get; set; } 
        public short? Kp { get; set; }
        public short? Imdb { get; set; }
        public short? FilmCritics { get; set; } 
        public short? RussianFilmCritics { get; set; } 
        public List<MovieEntity> Movies { get; set; }
        public override bool Equals(Object? obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != typeof(RatingEntity))
                return false;
            
            RatingEntity? otherRatingEntity = obj as RatingEntity;
            if (otherRatingEntity == null)
                return false;

            if ( Kp == otherRatingEntity.Kp &&
                 Imdb == otherRatingEntity.Imdb &&
                 FilmCritics == otherRatingEntity.FilmCritics)
            {
                return true;
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return Kp.GetHashCode();
        }

        public double GetAvg(RatingEntity otherRatingEntity)
        {
            double kpValue = otherRatingEntity.Kp ?? 0;
            double imdbValue = otherRatingEntity.Imdb ?? 0;
            double filmCriticsValue = otherRatingEntity.FilmCritics ?? 0;
            
            return Math.Round((kpValue + imdbValue + filmCriticsValue) / 3.0, 2);
        }
        
    }
}