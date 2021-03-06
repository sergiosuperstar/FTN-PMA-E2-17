/*
 * Simple TripApp API
 *
 * This is a simple TripApp API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class TicketPurchase :  IEquatable<TicketPurchase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketPurchase" /> class.
        /// </summary>
        /// <param name="Id">Id (required).</param>
        /// <param name="Code">Code (required).</param>
        /// <param name="Price">Price.</param>
        /// <param name="StartDateTime">StartDateTime (required).</param>
        /// <param name="EndDateTime">EndDateTime (required).</param>
        /// <param name="NumberOfPassangers">Number of passangers allowed to travel with buyer and including buyer too..</param>
        /// <param name="Type">Type (required).</param>
        /// <param name="User">User (required).</param>
        public TicketPurchase(int? Id = null, Guid? Code = null, double? Price = null, DateTime? StartDateTime = null, DateTime? EndDateTime = null, int? NumberOfPassangers = null, TicketType Type = null, User User = null)
        {
            // to ensure "Id" is required (not null)
            if (Id == null)
            {
                throw new InvalidDataException("Id is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.Id = Id;
            }
            // to ensure "Code" is required (not null)
            if (Code == null)
            {
                throw new InvalidDataException("Code is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.Code = Code;
            }
            // to ensure "StartDateTime" is required (not null)
            if (StartDateTime == null)
            {
                throw new InvalidDataException("StartDateTime is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.StartDateTime = StartDateTime;
            }
            // to ensure "EndDateTime" is required (not null)
            if (EndDateTime == null)
            {
                throw new InvalidDataException("EndDateTime is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.EndDateTime = EndDateTime;
            }
            // to ensure "Type" is required (not null)
            if (Type == null)
            {
                throw new InvalidDataException("Type is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.Type = Type;
            }
            // to ensure "User" is required (not null)
            if (User == null)
            {
                throw new InvalidDataException("User is a required property for TicketPurchase and cannot be null");
            }
            else
            {
                this.User = User;
            }
            this.Price = Price;
            this.NumberOfPassangers = NumberOfPassangers;
            
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name="code")]
        public Guid? Code { get; set; }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name="price")]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or Sets StartDateTime
        /// </summary>
        [DataMember(Name="startDateTime")]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Gets or Sets EndDateTime
        /// </summary>
        [DataMember(Name="endDateTime")]
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Number of passangers allowed to travel with buyer and including buyer too.
        /// </summary>
        /// <value>Number of passangers allowed to travel with buyer and including buyer too.</value>
        [DataMember(Name="numberOfPassangers")]
        public int? NumberOfPassangers { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name="type")]
        public TicketType Type { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name="user")]
        public User User { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TicketPurchase {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  StartDateTime: ").Append(StartDateTime).Append("\n");
            sb.Append("  EndDateTime: ").Append(EndDateTime).Append("\n");
            sb.Append("  NumberOfPassangers: ").Append(NumberOfPassangers).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TicketPurchase)obj);
        }

        /// <summary>
        /// Returns true if TicketPurchase instances are equal
        /// </summary>
        /// <param name="other">Instance of TicketPurchase to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TicketPurchase other)
        {

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    this.Id == other.Id ||
                    this.Id != null &&
                    this.Id.Equals(other.Id)
                ) && 
                (
                    this.Code == other.Code ||
                    this.Code != null &&
                    this.Code.Equals(other.Code)
                ) && 
                (
                    this.Price == other.Price ||
                    this.Price != null &&
                    this.Price.Equals(other.Price)
                ) && 
                (
                    this.StartDateTime == other.StartDateTime ||
                    this.StartDateTime != null &&
                    this.StartDateTime.Equals(other.StartDateTime)
                ) && 
                (
                    this.EndDateTime == other.EndDateTime ||
                    this.EndDateTime != null &&
                    this.EndDateTime.Equals(other.EndDateTime)
                ) && 
                (
                    this.NumberOfPassangers == other.NumberOfPassangers ||
                    this.NumberOfPassangers != null &&
                    this.NumberOfPassangers.Equals(other.NumberOfPassangers)
                ) && 
                (
                    this.Type == other.Type ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                ) && 
                (
                    this.User == other.User ||
                    this.User != null &&
                    this.User.Equals(other.User)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                    if (this.Id != null)
                    hash = hash * 59 + this.Id.GetHashCode();
                    if (this.Code != null)
                    hash = hash * 59 + this.Code.GetHashCode();
                    if (this.Price != null)
                    hash = hash * 59 + this.Price.GetHashCode();
                    if (this.StartDateTime != null)
                    hash = hash * 59 + this.StartDateTime.GetHashCode();
                    if (this.EndDateTime != null)
                    hash = hash * 59 + this.EndDateTime.GetHashCode();
                    if (this.NumberOfPassangers != null)
                    hash = hash * 59 + this.NumberOfPassangers.GetHashCode();
                    if (this.Type != null)
                    hash = hash * 59 + this.Type.GetHashCode();
                    if (this.User != null)
                    hash = hash * 59 + this.User.GetHashCode();
                return hash;
            }
        }

        #region Operators

        public static bool operator ==(TicketPurchase left, TicketPurchase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TicketPurchase left, TicketPurchase right)
        {
            return !Equals(left, right);
        }

        #endregion Operators

    }
}
