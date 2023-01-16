namespace DataMapper
{
    using System.Configuration;
    using DataMapper.SqlServerDAO;

    public static class DAOFactoryMethod
    {
        private static readonly IDAOFactory CurrentDAOFactoryValue;

        static DAOFactoryMethod()
        {
            string currentDataProvider = ConfigurationManager.AppSettings["dataProvider"];
            if (string.IsNullOrWhiteSpace(currentDataProvider))
            {
                CurrentDAOFactoryValue = null;
            }
            else
            {
                switch (currentDataProvider.ToLower().Trim())
                {
                    case "sqlserver":
                        CurrentDAOFactoryValue = new SQLServerDAOFactory();
                        break;
                    case "oracle":
                        CurrentDAOFactoryValue = null;
                        return;
                    default:
                        CurrentDAOFactoryValue = new SQLServerDAOFactory();
                        break;
                }
            }
        }

        public static IDAOFactory CurrentDAOFactory
        {
            get
            {
                return CurrentDAOFactoryValue;
            }
        }
    }
}
