namespace FunctionApp3
{
    public class TodoUpdateModel
    {
        public string id;
        public string firstName;
        public string lastName;
        public string hireDate;
        public string dischargeDate;
        public string position;
        public string managerId;
        public object IsCompleted { get; internal set; }
        public string TaskDescription { get; internal set; }
    }
}