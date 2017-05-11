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

namespace IO.Swagger.Models
{
    [DataContract]
    public partial class PurchaseCode :  IEquatable<PurchaseCode>
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseCode" /> class.
        /// </summary>
        /// <param name="Id">Id (required).</param>
        /// <param name="Code">Code (required).</param>
        /// <param name="Value">Value (required).</param>
        /// <param name="GenarationDateTime">GenarationDateTime (required).</param>
        /// <param name="UsageDateTime">UsageDateTime (required).</param>
        /// <param name="Used">Already used by a user. (required).</param>
        /// <param name="User">User.</param>
        public PurchaseCode(int? Id = null, Guid? Code = null, double? Value = null, DateTime? GenarationDateTime = null, DateTime? UsageDateTime = null, bool? Used = null, User User = null)
        {
            // to ensure "Id" is required (not null)
            if (Id == null)
            {
                throw new InvalidDataException("Id is a required property for PurchaseCode and cannot be null");
            }
            else
            {
                this.Id = Id;
            }
            // to ensure "Code" is required (not null)
            if (Code == null)
            {
                throw new InvalidDataException("Code is a required property for PurchaseCode and cannot be null");
            }
            else
            {
                this.Code = Code;
            }
            // to ensure "Value" is required (not null)
            if (Value == null)
            {
                throw new InvalidDataException("Value is a required property for PurchaseCode and cannot be null");
            }
            else
            {
                this.Value = Value;
            }
            // to ensure "GenarationDateTime" is required (not null)
            if (GenarationDateTime == null)
            {
                throw new InvalidDataException("GenarationDateTime is a required property for PurchaseCode and cannot be null");
            }
            else
            {
                this.GenarationDateTime = GenarationDateTime;
            }
            // to ensure "Used" is required (not null)
            if (Used == null)
            {
                throw new InvalidDataException("Used is a required property for PurchaseCode and cannot be null");
            }
            else
            {
                this.Used = Used;
            }
            this.UsageDateTime = UsageDateTime;
            this.User = User;
            
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        public int? Id { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name="code")]
        public Guid? Code { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name="value")]
        public double? Value { get; set; }

        /// <summary>
        /// Gets or Sets GenarationDateTime
        /// </summary>
        [DataMember(Name="genarationDateTime")]
        public DateTime? GenarationDateTime { get; set; }

        /// <summary>
        /// Gets or Sets UsageDateTime
        /// </summary>
        [DataMember(Name="usageDateTime")]
        public DateTime? UsageDateTime { get; set; }

        /// <summary>
        /// Already used by a user.
        /// </summary>
        /// <value>Already used by a user.</value>
        [DataMember(Name="used")]
        public bool? Used { get; set; }

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
            sb.Append("class PurchaseCode {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  GenarationDateTime: ").Append(GenarationDateTime).Append("\n");
            sb.Append("  UsageDateTime: ").Append(UsageDateTime).Append("\n");
            sb.Append("  Used: ").Append(Used).Append("\n");
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
            return Equals((PurchaseCode)obj);
        }

        /// <summary>
        /// Returns true if PurchaseCode instances are equal
        /// </summary>
        /// <param name="other">Instance of PurchaseCode to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PurchaseCode other)
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
                    this.Value == other.Value ||
                    this.Value != null &&
                    this.Value.Equals(other.Value)
                ) && 
                (
                    this.GenarationDateTime == other.GenarationDateTime ||
                    this.GenarationDateTime != null &&
                    this.GenarationDateTime.Equals(other.GenarationDateTime)
                ) && 
                (
                    this.UsageDateTime == other.UsageDateTime ||
                    this.UsageDateTime != null &&
                    this.UsageDateTime.Equals(other.UsageDateTime)
                ) && 
                (
                    this.Used == other.Used ||
                    this.Used != null &&
                    this.Used.Equals(other.Used)
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
                    if (this.Value != null)
                    hash = hash * 59 + this.Value.GetHashCode();
                    if (this.GenarationDateTime != null)
                    hash = hash * 59 + this.GenarationDateTime.GetHashCode();
                    if (this.UsageDateTime != null)
                    hash = hash * 59 + this.UsageDateTime.GetHashCode();
                    if (this.Used != null)
                    hash = hash * 59 + this.Used.GetHashCode();
                    if (this.User != null)
                    hash = hash * 59 + this.User.GetHashCode();
                return hash;
            }
        }

        #region Operators

        public static bool operator ==(PurchaseCode left, PurchaseCode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PurchaseCode left, PurchaseCode right)
        {
            return !Equals(left, right);
        }

        #endregion Operators

    }
}