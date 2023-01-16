namespace DataMapper
{
    public interface IDAOFactory
    {
        IUserDataServices UserDataServices { get; }

        IProductDataServices ProducDataServices { get; }

        IUserAuctionDataServices UserAuctionDataServices { get; }

        ICategoryDataServices CategoryDataServices { get; }

        ICategoryRelationDataServices CategoryRelationDataServices { get; }
    }
}
