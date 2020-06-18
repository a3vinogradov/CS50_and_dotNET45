using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo
{
  public class MenuCard
  {
    public int Id { get; set; }
    public string Text { get; set; }
    public virtual ICollection<Menu> Menus { get; set; }
  }
}
