namespace DomainModel
{
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class CategoryRelation
    {
        [NotNullValidator]
        public Category ParentCategory { get; set; }

        [NotNullValidator]
        public Category ChildCategory { get; set; }
    }
}
