using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMI.Models
{

    [MetadataType(typeof(MetaOportundiad))]
  
    public partial class TbOportunidad
    {
        public int derivado { get; set; }
    }

    public partial class MetaOportundiad : TbOportunidad
    {

    }
}