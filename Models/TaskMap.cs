using FluentNHibernate.Mapping;

namespace TaskAPI.Models
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap() {

            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.IsComplete);
            Table("Task_tbl");
        }
    }
}
