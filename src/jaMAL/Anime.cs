﻿/****************************************************************************
 * jaMAL
 * Copyright (c) Emmanuel Rojas Fredini, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jaMAL
{
    /// <summary>
    /// Anime information
    /// </summary>
    public class Anime : Media
    {
        #region Properties

        protected uint _episodes;
        public uint Episodes
        {
            get
            {
                return _episodes;
            }
            internal set
            {
                _episodes = value;
                RaisePropertyChanged("Episodes");
            }
        }

        #endregion

        #region Methods

        internal Anime(uint id, string name)
        {
            _id = id;
            _name = name;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Anime return false.
            Anime a = obj as Anime;
            if ((System.Object)a == null)
            {
                return false;
            }

            // Return true if the fields match:
            return base.Equals(a) && (_episodes == a._episodes);
        }

        public bool Equals(Anime a)
        {
            // If parameter is null return false:
            if ((object)a == null)
            {
                return false;
            }

            // Return true if the fields match:
            return base.Equals(a) && (_episodes == a._episodes);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }

}
