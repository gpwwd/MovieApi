namespace MovieApiMvc.ErrorHandling;

public sealed class ImageNotFoundException : NotFoundException
{
    public ImageNotFoundException(Guid imageId)
        :base ($"The image with id: {imageId} doesn't exist in the database.")
    {
    }
}