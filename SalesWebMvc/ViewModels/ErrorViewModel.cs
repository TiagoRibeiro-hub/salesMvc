namespace SalesWebMvc.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string? Message { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string SomethingWentWrong()
        {
            return Message = "Something Went Wrong";
        }
        public string SomethingWentWrongWithDelete()
        {
            return Message = "Something Went Wrong With Delete Operation";
        }
        public string SellerIsAlreadyRegistered()
        {
            return Message = "Seller Is Already Registered";
        }
        public string DepartmentIsAlreadyRegistered()
        {
            return Message = "Department Is Already Registered";
        }
        public string ChooseDepartmentOption()
        {
            return Message = "Department Is Required";
        }
        public string CanNotDeleteSeller(string name)
        {
            return Message = $"Can't delete {name} because has sales";
        }
    }
}