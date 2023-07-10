namespace AddressBook.Domain.Dtos.Room
{
    public sealed class GetByIdRoomWithEpiuqmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string[] EquipmentNames { get; set; }
    }

    public sealed class GetByIdRoomWithEpiuqmentDataDto
    {
        public string EquipmentNames { get; set; }
    }
}
