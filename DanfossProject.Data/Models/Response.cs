namespace DanfossProject.Data.Models
{
	public class Response
	{
		public bool Ok { get; set; }
		public string Message { get; set; }

		public Response()
		{
			Ok = false;
			Message = string.Empty;
		}
	}
}
