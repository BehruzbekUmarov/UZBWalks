using UZBWalks.Api.Models.Domain;

namespace UZBWalks.Api.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
