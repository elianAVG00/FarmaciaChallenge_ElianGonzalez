﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaVerifarmaChallenge.Domain.Entities
{
    public class Farmacia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        [Column(TypeName = "decimal(8, 5)")] //maximo num posible 90 + 5 digitos + digito para negativo = 8
        public decimal Latitud { get; set; }
        [Column(TypeName = "decimal(9, 5)")] //maximo num posible 180
        public decimal Longitud { get; set; }
    }
}
