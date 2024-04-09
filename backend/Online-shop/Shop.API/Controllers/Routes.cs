namespace Shop.API.Controllers
{
    public static class Routes
    {
        private const string BaseApi = "api/";
        public static class Items
        {
            private const string BaseApi = Routes.BaseApi + "items/";

            public const string GetItems = BaseApi + "list";
            public const string GetItemById = BaseApi + "{itemId:int}/details";
            public const string UpdateItemById = BaseApi + "{itemId::int}/update";
            public const string DeleteItemById = BaseApi + "{itemId::int}/delete";
            public const string CreateItem = BaseApi + "create";

            public static class Categories
            {
                public const string GetCategories = BaseApi + "categories/list";
            }
        }
    }
}
